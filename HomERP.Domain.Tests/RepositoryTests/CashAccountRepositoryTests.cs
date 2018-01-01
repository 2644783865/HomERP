using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

using HomERP.Domain.Repository.Abstract;
using HomERP.Domain.Repository.EntityFramework;
using HomERP.Domain.Entity;
using System.Threading.Tasks;

namespace HomERP.Domain.Tests.RepositoryTests
{
    [TestClass]
    public class CashAccountRepositoryTests
    {
        private EfDbContext context;
        private ICashAccountRepository repository;

        [TestInitialize]
        public void Initialize()
        {
            context = new EfDbContext(HomERP.Domain.Tests.Context.MemoryContext.GenerateContextOptions());
            repository = new EfCashAccountRepository(context);
        }

        [TestMethod]
        public async Task CashAccountRepository_Should_Add_CashAccount_To_Context_When_Saving_Repository()
        {
            //arrange
            CashAccount account = new CashAccount
            {
                Name = "Konto",
                InitialAmount = 123.45m,
                Family = new Family { Id = 1, Name = "Rodzinka" }
            };
            //act
            bool result = await repository.SaveCashAccountAsync(account);
            //assert
            result.Should().BeTrue();
            //check if object has been written to the context
            context.CashAccounts.Count().Should().Be(1, "when we add one object to empty collection, there should be only this one.");
            var resultAccount = context.CashAccounts.FirstOrDefault();
            resultAccount.Name.Should().Be(account.Name);
            resultAccount.InitialAmount.Should().Be(account.InitialAmount);
        }

        [TestMethod]
        public async Task CashAccountRepository_Should_Update_Context_When_Updating_CashAccount()
        {
            //arrange
            CashAccount account = new CashAccount()
            {
                Name = "Portfel",
                InitialAmount = 0,
                Family = new Family { Id = 1, Name = "Rodzinka" }
            };
            context.Families.Add(new Family { Id = 1, Name = "Rodzinka" });
            context.SaveChanges();
            await repository.SaveCashAccountAsync(account);
            CashAccount testAccount = repository.CashAccounts.Where(a => a.Name == "Portfel").First();
            testAccount.Name = "Portfel Zenka";
            testAccount.InitialAmount = 120;
            //act
            bool result = await repository.SaveCashAccountAsync(testAccount);
            //assert
            result.Should().BeTrue();
            context.CashAccounts.Count().Should().Be(1);
            CashAccount resultAccount = context.CashAccounts.Where(a => a.Name == testAccount.Name).First();
            resultAccount.Name.Should().Be(testAccount.Name);
            resultAccount.InitialAmount.Should().Be(testAccount.InitialAmount);
        }

        [TestMethod]
        public async Task CashAccountRepository_Should_Delete_Account()
        {
            //arrange
            CashAccount account = new CashAccount()
            {
                Name = "Portfel",
                InitialAmount = 0,
                Family = new Family { Id = 1, Name = "Rodzinka" }
            };
            await repository.SaveCashAccountAsync(account);
            int id = context.CashAccounts.First().Id;
            //act
            int result = await repository.DeleteRangeAsync(new int[] { id });
            //assert
            result.Should().Be(1);
            id.Should().NotBe(0, "i already added an CashAccount to repository, so it should be written to context.");
            context.CashAccounts.Count().Should().Be(0);
        }

        [TestMethod]
        public async Task CashAccountRepository_Should_Fail_Deleting_NonExistent_Account()
        {
            //arrange
            CashAccount account = new CashAccount()
            {
                Name = "Portfel",
                InitialAmount = 0,
                Family = new Family { Id = 1, Name = "Rodzinka" }
            };
            await repository.SaveCashAccountAsync(account);
            int id = 5;
            //act
            int result = await repository.DeleteRangeAsync(new int[] { id });
            //assert
            result.Should().Be(0);
            context.CashAccounts.Count().Should().Be(1);
        }
        [TestMethod]

        public async Task CashAccountRepository_Should_Fail_Deleting_NotEmpty_Account()
        {
            //arrange
            var ctx = HomERP.Domain.Tests.Context.SampleEntities.Context;
            int id = ctx.CashAccounts.First().Id;
            //act
            int result = await repository.DeleteRangeAsync(new int[] { id });
            //assert
            result.Should().Be(0);
            ctx.CashAccounts.Count().Should().Be(3);
        }
    }
}
