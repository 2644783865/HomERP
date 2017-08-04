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


namespace HomERP.WebUI.Tests
{
    [TestClass]
    public class AccountControllerTests
    {
        [TestMethod]
        public void Should_Call_AccountProvider_When_Showing_Accounts()
        {
            //arrange
            Mock<IAccountProvider> mock = new Mock<IAccountProvider>();
            mock.Setup(m => m.Accounts).Returns(new Account[]
            {
                new Account{ Id=1, InitialAmount=10, Name="Konto" },
                new Account{ Id=2, InitialAmount=20, Name="Konto2"}
            });
            CashAccountController controller = new CashAccountController(mock.Object);
            //act
            var result = controller.Index();
            //assert
            mock.VerifyGet(m => m.Accounts);
            result.Should().BeOfType<ViewResult>();
        }

        [TestMethod]
        public void Should_Get_Exact_Account_When_Edit()
        {
            //arrange
            Mock<IAccountProvider> mock = this.GenerateMockAccountProvider();
            CashAccountController controller = new CashAccountController(mock.Object);
            //act
            var result = controller.Edit(2);
            //assert
            mock.Verify(m => m.Accounts);
            result.Should().BeOfType<ViewResult>();
            ((ViewResult)result).Model.Should().BeOfType<Account>();
            ((Account)((ViewResult)result).Model).Name.Should().Be("Konto2");
        }

        [TestMethod]
        public void Should_Modify_Account_When_Correct_Account_Given()
        {
            //arrange
            Mock<IAccountProvider> mock = this.GenerateMockAccountProvider();
            CashAccountController controller = new CashAccountController(mock.Object);
            Account accToEdit = mock.Object.Accounts.First();
            accToEdit.Name = "Konto zmienione";
            //act
            var result = controller.Edit(accToEdit);
            //assert
            mock.Verify(m => m.SaveAccount(accToEdit));
            result.Should().BeOfType<ViewResult>();
            ((ViewResult)result).Model.Should().BeOfType<Account[]>();
        }

        [TestMethod]
        public void Should_Return_Deleted_Account()
        {
            //arrange
            Mock<IAccountProvider> mock = this.GenerateMockAccountProvider();
            mock.Setup(m => m.DeleteAccount(1)).Returns(mock.Object.Accounts.First());
            CashAccountController controller = new CashAccountController(mock.Object);
            Account accToDelete = mock.Object.Accounts.First();
            //act
            var result = controller.Delete(accToDelete.Id);
            //arrange
            mock.Verify(m => m.DeleteAccount(accToDelete.Id));
            result.Should().BeOfType<ViewResult>();
            ((ViewResult)result).Model.Should().BeOfType<Account[]>();
            ((Account[])((ViewResult)result).Model)[0].Id.Should().Be(accToDelete.Id);
        }

        private Mock<IAccountProvider> GenerateMockAccountProvider()
        {
            Mock<IAccountProvider> mock = new Mock<IAccountProvider>();
            mock.Setup(m => m.Accounts).Returns(new Account[]
            {
                new Account{ Id=1, InitialAmount=10, Name="Konto" },
                new Account{ Id=2, InitialAmount=20, Name="Konto2"}
            });
            return mock;
        }
    }
}
