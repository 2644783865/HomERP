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
    public class AccountLogicTests
    {
        private Account PrepareExampleAccount()
        {
            return new Account
            {
                InitialAmount = 50,
                Name = "Portfel"
            };
        }

        [TestMethod]
        public void Test_GetAccounts()
        {
            //arrange
            Mock<IAccountRepository> mock = new Mock<IAccountRepository>();
            mock.Setup(m => m.Accounts).Returns(new Account[]
            {
                new Account { InitialAmount=100, Name = "Konto1"},
                new Account { InitialAmount=65.02m, Name = "Konto2"}
            }
                );
            AccountProvider provider = new AccountProvider(mock.Object);

            //act
            IEnumerable<Account> Accounts = provider.Accounts;

            //assert
            Accounts.Should().HaveCount(2, "you have 2 entities in the repository");
        }

        [TestMethod]
        public void Test_SaveAccount()
        {
            //arrange
            Account account = PrepareExampleAccount();
            Mock<IAccountRepository> mock = new Mock<IAccountRepository>();
            mock.Setup(m => m.Accounts).Returns(new Account[]
            {
                new Account { InitialAmount=100, Name = "Konto1"},
                new Account { InitialAmount=65.02m, Name = "Konto2"}
            }
                );
            AccountProvider provider = new AccountProvider(mock.Object);

            //act
            provider.SaveAccount(account);

            //assert
            //In this place we have to focus on that underlying repository method has been properly called
            //instead of wandering if entity has been properly saved - this is the repository responsibility.
            mock.Verify(m => m.SaveAccount(account));
        }

        [TestMethod]
        public void Test_DeleteAccount()
        {
            //arrange
            Mock<IAccountRepository> mock = new Mock<IAccountRepository>();
            mock.Setup(m => m.Accounts).Returns(new Account[]
            {
                new Account { Id = 1, InitialAmount=100, Name = "Konto1"},
                new Account { Id = 2, InitialAmount=65.02m, Name = "Konto2"}
            }
                );
            Account accountToDelete = mock.Object.Accounts.Where(p => p.Id == 2).First();
            AccountProvider provider = new AccountProvider(mock.Object);

            //act
            provider.DeleteAccount(accountToDelete.Id);

            //assert if repository delete method has been called with proper identifier
            mock.Verify(m => m.DeleteAccount(2));
        }
    }
}
