using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using FluentAssertions;

using HomERP.Domain.Logic.Abstract;
using HomERP.Domain.Entity;
using HomERP.WebUI.Controllers;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;

namespace HomERP.WebUI.Tests
{
    [TestClass]
    public class CashAccountControllerTests
    {
        [TestMethod]
        public void Should_Call_CashAccountProvider_When_Showing_CashAccounts()
        {
            //arrange
            Mock<ICashAccountProvider> mock = new Mock<ICashAccountProvider>();
            mock.Setup(m => m.CashAccounts).Returns(new CashAccount[]
            {
                new CashAccount{ Id=1, InitialAmount=10, Name="Konto" },
                new CashAccount{ Id=2, InitialAmount=20, Name="Konto2"}
            }.AsQueryable());
            CashAccountController controller = new CashAccountController(mock.Object);
            //act
            var result = controller.Index();
            //assert
            mock.VerifyGet(m => m.CashAccounts);
            result.Should().BeOfType<ViewResult>();
        }

        [TestMethod]
        public void Should_Get_Exact_CashAccount_When_Edit()
        {
            //arrange
            Mock<ICashAccountProvider> mock = this.GenerateMockCashAccountProvider();
            CashAccountController controller = new CashAccountController(mock.Object);
            //act
            var result = controller.Edit(2);
            //assert
            mock.Verify(m => m.CashAccounts);
            result.Should().BeOfType<ViewResult>();
            ((ViewResult)result).Model.Should().BeOfType<CashAccount>();
            ((CashAccount)((ViewResult)result).Model).Name.Should().Be("Konto2");
        }

        [TestMethod]
        public void Should_Modify_CashAccount_When_Correct_CashAccount_Given()
        {
            //arrange
            Mock<ICashAccountProvider> mock = this.GenerateMockCashAccountProvider();
            CashAccountController controller = new CashAccountController(mock.Object);
            CashAccount accToEdit = mock.Object.CashAccounts.First();
            accToEdit.Name = "Konto zmienione";
            //act
            var result = controller.Edit(accToEdit);
            //assert
            mock.Verify(m => m.SaveCashAccount(accToEdit));
            result.Should().BeOfType<ViewResult>();
            ((ViewResult)result).Model.Should().BeOfType<CashAccount[]>();
        }

        [TestMethod]
        public void Should_Return_Deleted_CashAccount()
        {
            //arrange
            Mock<ICashAccountProvider> mock = this.GenerateMockCashAccountProvider();
            mock.Setup(m => m.DeleteCashAccount(1)).Returns(mock.Object.CashAccounts.First());
            CashAccountController controller = new CashAccountController(mock.Object);
            CashAccount accToDelete = mock.Object.CashAccounts.First();
            //act
            var result = controller.Delete(accToDelete.Id);
            //arrange
            mock.Verify(m => m.DeleteCashAccount(accToDelete.Id));
            result.Should().BeOfType<ViewResult>();
            ((ViewResult)result).Model.Should().BeOfType<CashAccount[]>();
            ((CashAccount[])((ViewResult)result).Model)[0].Id.Should().Be(accToDelete.Id);
        }

        private Mock<ICashAccountProvider> GenerateMockCashAccountProvider()
        {
            Mock<ICashAccountProvider> mock = new Mock<ICashAccountProvider>();
            mock.Setup(m => m.CashAccounts).Returns(new CashAccount[]
            {
                new CashAccount{ Id=1, InitialAmount=10, Name="Konto" },
                new CashAccount{ Id=2, InitialAmount=20, Name="Konto2"}
            }.AsQueryable());
            return mock;
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
