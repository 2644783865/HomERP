using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

using HomERP.Domain.Repository.Abstract;
using HomERP.Domain.Repository.EntityFramework;
using HomERP.Domain.Entity;

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
        public void Should_Add_CashAccount_To_Context_When_Saving_Repository()
        {
            //arrange
            CashAccount account = new CashAccount
            {
                Name = "Konto",
                InitialAmount = 123.45m,
                Family = new Family { Id = 1, Name = "Rodzinka" }
            };
            //act
            repository.SaveCashAccount(account);
            //assert
            //check if object has been written to the context
            context.CashAccounts.Count().Should().Be(1, "when we add one object to empty collection, there should be only this one.");
            var resultAccount = context.CashAccounts.FirstOrDefault();
            resultAccount.Name.Should().Be(account.Name);
            resultAccount.InitialAmount.Should().Be(account.InitialAmount);
        }

        [TestMethod]
        public void Should_Update_Context_When_Updating_CashAccount()
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
            repository.SaveCashAccount(account);
            CashAccount testAccount = repository.CashAccounts.Where(a => a.Name == "Portfel").First();
            testAccount.Name = "Portfel Zenka";
            testAccount.InitialAmount = 120;
            //act
            repository.SaveCashAccount(testAccount);
            //assert
            context.CashAccounts.Count().Should().Be(1);
            CashAccount resultAccount = context.CashAccounts.Where(a => a.Name == testAccount.Name).First();
            resultAccount.Name.Should().Be(testAccount.Name);
            resultAccount.InitialAmount.Should().Be(testAccount.InitialAmount);
        }

        [TestMethod]
        public void Should_Return_Deleted_CashAccount_When_Deleting_From_Repository()
        {
            //arrange
            CashAccount account = new CashAccount()
            {
                Name = "Portfel",
                InitialAmount = 0,
                Family = new Family { Id = 1, Name = "Rodzinka" }
            };
            repository.SaveCashAccount(account);
            //act
            int id = context.CashAccounts.First().Id;
            CashAccount deletedAccount = repository.DeleteCashAccount(id);
            //assert
            id.Should().NotBe(0, "i already added an CashAccount to repository, so it should be written to context.");
            deletedAccount.Id.Should().Be(id);
            deletedAccount.Name.Should().Be(account.Name);
            context.CashAccounts.Count().Should().Be(0);
        }
    }
}
