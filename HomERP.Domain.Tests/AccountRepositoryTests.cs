using System;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Effort;

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
            //this.context = Effort.ObjectContextFactory.CreateTransient<EfDbContext>();
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
            Assert.AreEqual(1, context.Accounts.Count());
            var resultAccount = context.Accounts.FirstOrDefault();
            Assert.AreEqual("Konto", resultAccount.Name);
            Assert.AreEqual(123.45m, resultAccount.InitialAmount);

        }
       

    }
}
