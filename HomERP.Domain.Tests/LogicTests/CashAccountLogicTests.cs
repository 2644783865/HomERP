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
using System.Threading.Tasks;
using System.Linq.Expressions;

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
        public void CashAccountProvider_Should_Get_CashAccounts_Only_Of_Own_Family()
        {
            //arrange
            Mock<ICashAccountRepository> mock = new Mock<ICashAccountRepository>();
            mock.Setup(m => m.CashAccounts).Returns(new CashAccount[]
            {
                new CashAccount { InitialAmount=100, Name = "Konto1", Family = new Family { Id = 1 } },
                new CashAccount { InitialAmount=65.02m, Name = "Konto2", Family = new Family { Id = 1 }},
                new CashAccount { InitialAmount=50.00m, Name = "Konto3", Family = new Family { Id = 2 }}
            }.AsQueryable());

            Mock<ISessionDataProvider> sessionProvider = new Mock<ISessionDataProvider>();
            sessionProvider.Setup(s => s.Family).Returns(new Family { Id = 1, Name = "Super Family" });
            CashAccountProvider provider = new CashAccountProvider(mock.Object, sessionProvider.Object);

            //act
            IEnumerable<CashAccount> CashAccounts = provider.CashAccounts;

            //assert
            CashAccounts.Should().HaveCount(2, "you have 2 account for your Family and one for another Family");
        }

        [TestMethod]
        public async Task CashAccountProvider_Should_Save_Account_Of_Own_Family()
        {
            //arrange
            Mock<ICashAccountRepository> repository = new Mock<ICashAccountRepository>();
            repository.Setup(r => r.SaveCashAccountAsync(It.IsAny<CashAccount>())).Returns(Task.FromResult<bool>(true));
            Mock<ISessionDataProvider> session = new Mock<ISessionDataProvider>();
            session.Setup(s => s.Family).Returns(new Family { Id = 1, Name = "Rodzinka" });
            CashAccountProvider provider = new CashAccountProvider(repository.Object, session.Object);
            CashAccount accountToSave = new CashAccount { Family = new Family { Id = 1 }, Name = "Konto" };
            //act
            bool result = await provider.SaveCashAccountAsync(accountToSave);
            //assert
            result.Should().BeTrue();
            session.Verify(s => s.Family);
            repository.Verify(r => r.SaveCashAccountAsync(accountToSave));
        }

        [TestMethod]
        public async Task CashAccountProvider_Should_Fail_Updating_Account_Of_Other_Family()
        {
            //arrange
            Mock<ICashAccountRepository> repository = new Mock<ICashAccountRepository>();
            repository.Setup(r => r.SaveCashAccountAsync(It.IsAny<CashAccount>())).Returns(Task.FromResult<bool>(true));
            Mock<ISessionDataProvider> session = new Mock<ISessionDataProvider>();
            session.Setup(s => s.Family).Returns(new Family { Id = 1, Name = "Rodzinka" });
            CashAccountProvider provider = new CashAccountProvider(repository.Object, session.Object);
            CashAccount accountToSave = new CashAccount { Family = new Family { Id = 2 }, Id = 1, Name = "Konto" };
            //act
            bool result = await provider.SaveCashAccountAsync(accountToSave);
            //assert
            result.Should().BeFalse();
            session.Verify(s => s.Family);
            repository.Verify(r => r.SaveCashAccountAsync(accountToSave), Times.Never);
        }

        [TestMethod]
        public async Task CashAccountProvider_Should_Update_Account_Of_Own_Family()
        {
            //arrange
            Mock<ICashAccountRepository> repository = new Mock<ICashAccountRepository>();
            repository.Setup(r => r.SaveCashAccountAsync(It.IsAny<CashAccount>())).Returns(Task.FromResult<bool>(true));
            Mock<ISessionDataProvider> session = new Mock<ISessionDataProvider>();
            session.Setup(s => s.Family).Returns(new Family { Id = 1, Name = "Rodzinka" });
            CashAccountProvider provider = new CashAccountProvider(repository.Object, session.Object);
            CashAccount accountToSave = new CashAccount { Family = new Family { Id = 1 }, Id = 1, Name = "Konto" };
            //act
            bool result = await provider.SaveCashAccountAsync(accountToSave);
            //assert
            result.Should().BeTrue();
            session.Verify(s => s.Family);
            repository.Verify(r => r.SaveCashAccountAsync(accountToSave));
        }

        [TestMethod]
        public async Task CashAccountProvider_Should_Delete_Own_Accounts()
        {
            //arrange
            Mock<ICashAccountRepository> repository = new Mock<ICashAccountRepository>();
            repository.Setup(r => r.DeleteRangeAsync(It.IsAny<IEnumerable<int>>())).Returns(Task.FromResult<int>(1));
            repository.Setup(m => m.CashAccounts).Returns(new CashAccount[]
            {
                new CashAccount { Id = 1, InitialAmount=100, Name = "Konto1", Family = new Family { Id = 1 } },
                new CashAccount { Id = 2, InitialAmount=65.02m, Name = "Konto2", Family = new Family { Id = 1 }},
                new CashAccount { Id = 3, InitialAmount=50.00m, Name = "Konto3", Family = new Family { Id = 2 }}
            }.AsQueryable());
            Mock<ISessionDataProvider> session = new Mock<ISessionDataProvider>();
            session.Setup(s => s.Family).Returns(new Family { Id = 1, Name = "Rodzinka" });
            CashAccountProvider provider = new CashAccountProvider(repository.Object, session.Object);
            IEnumerable<int> accountsToDelete = new int[] { 1 };
            //act
            bool result = await provider.DeleteRangeAsync(accountsToDelete);
            //assert
            result.Should().BeTrue();
            session.Verify(s => s.Family);
            repository.Verify(r => r.DeleteRangeAsync(accountsToDelete));
        }

        [TestMethod]
        public async Task CashAccountProvider_Should_Fail_Deleting_Account_Of_Other_Family()
        {
            //arrange
            Mock<ICashAccountRepository> repository = new Mock<ICashAccountRepository>();
            repository.Setup(r => r.DeleteRangeAsync(It.IsAny<IEnumerable<int>>())).Returns(Task.FromResult<int>(1));
            repository.Setup(m => m.CashAccounts).Returns(new CashAccount[]
            {
                new CashAccount { Id = 1, InitialAmount=100, Name = "Konto1", Family = new Family { Id = 1 } },
                new CashAccount { Id = 2, InitialAmount=65.02m, Name = "Konto2", Family = new Family { Id = 1 }},
                new CashAccount { Id = 3, InitialAmount=50.00m, Name = "Konto3", Family = new Family { Id = 2 }}
            }.AsQueryable());
            Mock<ISessionDataProvider> session = new Mock<ISessionDataProvider>();
            session.Setup(s => s.Family).Returns(new Family { Id = 1, Name = "Rodzinka" });
            CashAccountProvider provider = new CashAccountProvider(repository.Object, session.Object);
            IEnumerable<int> accountsToDelete = new int[] { 3 };
            //act
            bool result = await provider.DeleteRangeAsync(accountsToDelete);
            //assert
            result.Should().BeFalse();
            session.Verify(s => s.Family);
            repository.Verify(r => r.DeleteRangeAsync(It.IsAny<IEnumerable<int>>()), Times.Never);
        }
    }
}
