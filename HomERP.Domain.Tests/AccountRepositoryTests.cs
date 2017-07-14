using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using HomERP.Domain.Repository.Abstract;
using HomERP.Domain.Entity.Abstract;
using HomERP.Domain.Entity;

namespace HomERP.Domain.Tests
{
    [TestClass]
    public class AccountRepositoryTests
    {
        [TestMethod]
        public void Test()
        {
            Mock<IAccountRepository> mock = new Mock<Repository.Abstract.IAccountRepository>();
            mock.Setup(m => m.Accounts).Returns(new Account[] {
                new Account() { Name="Konto", InitialAmount=100.00m },
                new Account() { Name="Portfel", InitialAmount=123.45m },
                new Account() { Name="Skarbonka", InitialAmount=50.00m }
            });
        }
    }
}
