using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using FluentAssertions;
using FluentAssertions.AspNetCore.Mvc;
using HomERP.Domain.Logic.Abstract;
using HomERP.Domain.Entity;
using HomERP.WebUI.Controllers;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using HomERP.WebUI.Handlers.Abstract;
using HomERP.WebUI.Models.Shared;
using HomERP.WebUI.Models.CashAccount;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using HomERP.WebUI.Tests.Helpers;

namespace HomERP.WebUI.Tests.Controllers
{
    [TestClass]
    public class CashAccountControllerTests
    {
        [TestMethod]
        public void CashAccountController_Should_Call_CashAccountProvider_When_Showing_CashAccounts()
        {
            //arrange
            Mock<ICashAccountHandler> handler = new Mock<ICashAccountHandler>();
            handler.Setup(m => m.GetList(It.IsAny<PageInfo>())).Returns(new CashAccountListVM());
            CashAccountController controller = new CashAccountController(handler.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.HttpContext.Session = new SessionMock();
            //act
            var result = controller.Index(0);
            //assert
            handler.Verify(m => m.GetList(It.IsAny<PageInfo>()));
            result.Should().BeViewResult();
        }

        [TestMethod]
        public async Task CashAccountController_Should_Perform_Delete()
        {
            //arrange
            Mock<ICashAccountHandler> handler = new Mock<ICashAccountHandler>();
            handler.Setup(m => m.PerformDeletion(It.IsAny<IEnumerable<int>>())).Returns(Task.FromResult<Message>(new Message()));
            CashAccountController controller = new CashAccountController(handler.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.HttpContext.Session = new SessionMock();
            //act
            var result = await controller.GroupAction(1, new int[] { 1, 2 }, "Delete");
            //assert
            result.Should().BeOfType<RedirectToActionResult>();
            handler.Verify(m => m.PerformDeletion(new int[] { 1, 2 }));
        }

        [TestMethod]
        public async Task CashAccountController_Should_Take_No_Action()
        {
            //arrange
            Mock<ICashAccountHandler> handler = new Mock<ICashAccountHandler>();
            handler.Setup(m => m.PerformDeletion(It.IsAny<IEnumerable<int>>())).Returns(Task.FromResult<Message>(new Message()));
            CashAccountController controller = new CashAccountController(handler.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.HttpContext.Session = new SessionMock();
            //act
            var result = await controller.GroupAction(1, new int[] { 1, 2 }, "blah");
            //assert
            result.Should().BeOfType<RedirectToActionResult>();
            handler.Verify(m => m.PerformDeletion(It.IsAny<IEnumerable<int>>()), Times.Never);
        }

        [TestMethod]
        public void CashAccountController_Should_Call_Handler_Edit()
        {
            //arrange
            Mock<ICashAccountHandler> handler = new Mock<ICashAccountHandler>();
            handler.Setup(m => m.Edit(It.IsAny<int>())).Returns(new CashAccountVM());
            CashAccountController controller = new CashAccountController(handler.Object);
            //act
            var result = controller.Edit(132);
            //assert
            result.Should().BeViewResult();
            handler.Verify(m => m.Edit(132));
        }

        [TestMethod]
        public async Task CashAccountController_Should_Call_Handler_EditAsync()
        {
            //arrange
            Mock<ICashAccountHandler> handler = new Mock<ICashAccountHandler>();
            handler.Setup(m => m.EditAsync(It.IsAny<CashAccountVM>())).Returns(Task.FromResult<bool>(true));
            CashAccountController controller = new CashAccountController(handler.Object);
            CashAccountVM account = new CashAccountVM();
            //act
            var result = await controller.Edit(account);
            //assert
            result.Should().BeOfType<RedirectToActionResult>();
            handler.Verify(m => m.EditAsync(account));
        }

        [TestMethod]
        public async Task CashAccountController_Should_Not_Call_Handler_EditAsync_When_Invalid_ModelState()
        {
            //arrange
            Mock<ICashAccountHandler> handler = new Mock<ICashAccountHandler>();
            handler.Setup(m => m.EditAsync(It.IsAny<CashAccountVM>())).Returns(Task.FromResult<bool>(true));
            CashAccountController controller = new CashAccountController(handler.Object);
            CashAccountVM account = new CashAccountVM();
            controller.ModelState.AddModelError(string.Empty, "Zapis konta nie powiódł się!");
            //act
            var result = await controller.Edit(account);
            //assert
            result.Should().BeViewResult();
            handler.Verify(m => m.EditAsync(account), Times.Never);
        }

        [TestMethod]
        public async Task CashAccountController_Should_Show_Edit_Again_When_Database_Error()
        {
            //arrange
            Mock<ICashAccountHandler> handler = new Mock<ICashAccountHandler>();
            handler.Setup(m => m.EditAsync(It.IsAny<CashAccountVM>())).Returns(Task.FromResult<bool>(false));
            CashAccountController controller = new CashAccountController(handler.Object);
            CashAccountVM account = new CashAccountVM();
            //act
            var result = await controller.Edit(account);
            //assert
            result.Should().BeViewResult();
            handler.Verify(m => m.EditAsync(account));
        }

        [TestMethod]
        public void Verify_CashAccountController_Is_Decorated_With_Authorize_Attribute()
        {
            var type = typeof(CashAccountController);

            var classAttributes = type.GetTypeInfo().GetCustomAttributes(typeof(AuthorizeAttribute));

            classAttributes.Should().NotBeEmpty();
        }
    }
}
