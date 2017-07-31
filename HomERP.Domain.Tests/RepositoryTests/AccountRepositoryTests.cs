using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

using HomERP.Domain.Repository.Abstract;
using HomERP.Domain.Repository.EntityFramework;
using HomERP.Domain.Entity.Abstract;
using HomERP.Domain.Entity;

namespace HomERP.Domain.Tests.RepositoryTests
{
    [TestClass]
    public class AccountRepositoryTests
    {
        private EfDbContext context;
        private IAccountRepository repository;

        [TestInitialize]
        public void Initialize()
        {
            context = new EfDbContext(HomERP.Domain.Tests.Context.MemoryContext.GenerateContextOptions());
            repository = new EfAccountRepository(context);
        }

        [TestMethod]
        public void Should_Add_Account_To_Context_When_Saving_Repository()
        {
            //arrange
            Account account = new Account() { Name = "Konto", InitialAmount = 123.45m };
            //act
            repository.SaveAccount(account);
            //assert
            //check if object has been written to the context
            context.Accounts.Count().Should().Be(1, "when we add one object to empty collection, there should be only this one.");
            var resultAccount = context.Accounts.FirstOrDefault();
            resultAccount.Name.Should().Be(account.Name);
            resultAccount.InitialAmount.Should().Be(account.InitialAmount);
        }

        [TestMethod]
        public void Should_Update_Context_When_Updating_Account()
        {
            //arrange
            Account account = new Account() { Name = "Portfel", InitialAmount = 0, };
            repository.SaveAccount(account);
            Account testAccount = repository.Accounts.Where(a => a.Name == "Portfel").First();
            testAccount.Name = "Portfel Zenka";
            testAccount.InitialAmount = 120;
            //act
            repository.SaveAccount(testAccount);
            //assert
            context.Accounts.Count().Should().Be(1);
            Account resultAccount = context.Accounts.Where(a => a.Name == testAccount.Name).First();
            resultAccount.Name.Should().Be(testAccount.Name);
            resultAccount.InitialAmount.Should().Be(testAccount.InitialAmount);
        }

        [TestMethod]
        public void Should_Return_Deleted_Account_When_Deleting_From_Repository()
        {
            //arrange
            Account account = new Account() { Name = "Portfel", InitialAmount = 0 };
            repository.SaveAccount(account);
            //act
            int id = context.Accounts.First().Id;
            Account deletedAccount = repository.DeleteAccount(id);
            //assert
            id.Should().NotBe(0, "i already added an Account to repository, so it should be written to context.");
            deletedAccount.Id.Should().Be(id);
            deletedAccount.Name.Should().Be(account.Name);
            context.Accounts.Count().Should().Be(0);
        }
    }
}
