using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Moq;

using HomERP.Domain.Entity;
using HomERP.Domain.Repository.Abstract;
using HomERP.Domain.Logic.Abstract;
using HomERP.Domain.Logic;

namespace HomERP.Domain.Tests.LogicTests
{
    [TestClass]
    public class CashAccountLogicTests
    {
        private CashAccount PrepareExampleCashAccount()
        {
            return new CashAccount
            {
                InitialAmount = 50,
                Name = "Portfel"
            };
        }

        [TestMethod]
        public void Should_Get_All_CashAccounts()
        {
            //arrange
            Mock<ICashAccountRepository> mock = new Mock<ICashAccountRepository>();
            mock.Setup(m => m.CashAccounts).Returns(new CashAccount[]
            {
                new CashAccount { InitialAmount=100, Name = "Konto1"},
                new CashAccount { InitialAmount=65.02m, Name = "Konto2"}
            }
                );
            CashAccountProvider provider = new CashAccountProvider(mock.Object);

            //act
            IEnumerable<CashAccount> CashAccounts = provider.CashAccounts;

            //assert
            CashAccounts.Should().HaveCount(2, "you have 2 entities in the repository");
        }
    }
}
