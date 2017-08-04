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

        [TestMethod]
        public void Should_Call_UserProvider_SaveCashAccount()
        {
            //arrange
            CashAccount account = PrepareExampleCashAccount();
            Mock<ICashAccountRepository> mock = new Mock<ICashAccountRepository>();
            mock.Setup(m => m.CashAccounts).Returns(new CashAccount[]
            {
                new CashAccount { InitialAmount=100, Name = "Konto1"},
                new CashAccount { InitialAmount=65.02m, Name = "Konto2"}
            }
                );
            CashAccountProvider provider = new CashAccountProvider(mock.Object);

            //act
            provider.SaveCashAccount(account);

            //assert
            //In this place we have to focus on that underlying repository method has been properly called
            //instead of wandering if entity has been properly saved - this is the repository responsibility.
            mock.Verify(m => m.SaveCashAccount(account));
        }

        [TestMethod]
        public void Should_Call_UserProvider_DeleteCashAccount()
        {
            //arrange
            Mock<ICashAccountRepository> mock = new Mock<ICashAccountRepository>();
            mock.Setup(m => m.CashAccounts).Returns(new CashAccount[]
            {
                new CashAccount { Id = 1, InitialAmount=100, Name = "Konto1"},
                new CashAccount { Id = 2, InitialAmount=65.02m, Name = "Konto2"}
            }
                );
            CashAccount accountToDelete = mock.Object.CashAccounts.Where(p => p.Id == 2).First();
            CashAccountProvider provider = new CashAccountProvider(mock.Object);

            //act
            provider.DeleteCashAccount(accountToDelete.Id);

            //assert if repository delete method has been called with proper identifier
            mock.Verify(m => m.DeleteCashAccount(2));
        }
    }
}
