using HomERP.WebUI.Handlers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using FluentAssertions;
using HomERP.Domain.Logic.Abstract;
using HomERP.WebUI.Models.CashAccount;
using HomERP.Domain.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace HomERP.WebUI.Tests.Handlers
{
    [TestClass]
    public class CashAccountHandlerTests
    {
        [TestMethod]
        public void CashAccounhHandler_GetList_Should_Call_Provider()
        {
            //arrange
            Mock<ICashAccountProvider> provider = new Mock<ICashAccountProvider>();
            CashAccountHandler handler = new CashAccountHandler(provider.Object, null);
            //act
            var result = handler.GetList(new Models.Shared.PageInfo { TotalItems = 24, PageSize = 5, CurrentPage = 2 });
            //assert
            provider.Verify(c => c.CashAccounts);
            result.Should().BeOfType<CashAccountListVM>();
        }

        [TestMethod]
        public void CashAccounhHandler_Edit_Should_Return_EditVM()
        {
            //arrange
            Mock<ICashAccountProvider> provider = new Mock<ICashAccountProvider>();
            provider.Setup(p => p.CashAccounts).Returns(new CashAccount[]
            {
                new CashAccount { Family = new Family {Id = 1}, Id = 123, Name = "Konto" }
            }
            .AsQueryable());
            CashAccountHandler handler = new CashAccountHandler(provider.Object, null);
            //act
            var result = handler.Edit(123);
            //assert
            provider.Verify(c => c.CashAccounts);
            result.Should().BeOfType<CashAccountVM>();
        }

        [TestMethod]
        public async Task CashAccounhHandler_EditAsync_Should_Try_To_Save_Account()
        {
            //arrange
            Mock<ICashAccountProvider> provider = new Mock<ICashAccountProvider>();
            provider.Setup(p => p.SaveCashAccountAsync(It.IsAny<CashAccount>())).Returns(Task.FromResult(true));
            Mock<ISessionDataProvider> session = new Mock<ISessionDataProvider>();
            CashAccountHandler handler = new CashAccountHandler(provider.Object, session.Object);
            //act
            var result = await handler.EditAsync(new CashAccountVM());
            //assert
            provider.Verify(c => c.SaveCashAccountAsync(It.IsAny<CashAccount>()));
            result.Should().BeTrue();
        }

        [TestMethod]
        public async Task CashAccounhHandler_DeleteRangeAsync_Should_Try_To_Delete_AccountRange()
        {
            //arrange
            Mock<ICashAccountProvider> provider = new Mock<ICashAccountProvider>();
            provider.Setup(p => p.DeleteRangeAsync(It.IsAny<IEnumerable<int>>())).Returns(Task.FromResult(true));
            CashAccountHandler handler = new CashAccountHandler(provider.Object, null);
            //act
            var result = await handler.PerformDeletion(new int[] { 1, 2 });
            //assert
            provider.Verify(c => c.DeleteRangeAsync(It.IsAny<IEnumerable<int>>()));
            result.Type.Should().Be("success");
        }
    }
}
