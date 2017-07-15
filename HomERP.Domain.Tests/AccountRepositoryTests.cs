using System;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Effort;
using FluentAssertions;

using HomERP.Domain.Repository.Abstract;
using HomERP.Domain.Repository.EntityFramework;
using HomERP.Domain.Entity.Abstract;
using HomERP.Domain.Entity;

namespace HomERP.Domain.Tests
{
    [TestClass]
    public class AccountRepositoryTests
    {
        private EfDbContext context;
        private IAccountRepository repository;

        [TestInitialize]
        public void Initialize()
        {
            var connection = DbConnectionFactory.CreateTransient();
            this.context = new EfDbContext(connection);
            this.repository = new EFAccountRepository(this.context);
        }

        [TestMethod]
        public void Test()
        {
            //arrange
            Account account = new Account() { Name = "Konto", InitialAmount = 123.45m };
            //act
            repository.SaveAccount(account);
            //assert
            //check if object has been written to the context
            context.Accounts.Count().Should().Be(1, "when we add one object to empty collection, there should be only this one.");
            var resultAccount = context.Accounts.FirstOrDefault();
            resultAccount.Name.Should().Be("Konto");
            resultAccount.InitialAmount.Should().Be(123.45m);
        }
    }
}
