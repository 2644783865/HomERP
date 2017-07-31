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
    public class UserControllerTests
    {
        [TestMethod]
        public void Should_Call_UserProvider_When_Showing_Users()
        {
            Mock<IUserProvider> mock = GenerateMockUserProvider();
            UserController controller = new UserController(mock.Object);
            //act
            var result = controller.Index();
            //assert
            mock.VerifyGet(m => m.Users);
            result.Should().BeOfType<ViewResult>();
        }

        [TestMethod]
        public void Should_Get_Exact_User_When_Edit()
        {
            //arrange
            Mock<IUserProvider> mock = GenerateMockUserProvider();
            UserController controller = new UserController(mock.Object);
            //act
            var result = controller.Edit(2);
            //assert
            mock.Verify(m => m.Users);
            result.Should().BeOfType<ViewResult>();
            ((ViewResult)result).Model.Should().BeOfType<User>();
            ((User)((ViewResult)result).Model).Name.Should().Be("Alfred");
        }

        [TestMethod]
        public void Should_Modify_User_When_Correct_User_Given()
        {
            //arrange
            Mock<IUserProvider> mock = GenerateMockUserProvider();
            UserController controller = new UserController(mock.Object);
            User accToEdit = mock.Object.Users.First();
            accToEdit.Name = "Anzelm";
            //act
            var result = controller.Edit(accToEdit);
            //assert
            mock.Verify(m => m.SaveUser(accToEdit));
            result.Should().BeOfType<ViewResult>();
            ((ViewResult)result).Model.Should().BeOfType<User[]>();
        }

        [TestMethod]
        public void Should_Return_Deleted_User()
        {
            //arrange
            Mock<IUserProvider> mock = GenerateMockUserProvider();
            mock.Setup(m => m.DeleteUser(1)).Returns(mock.Object.Users.First());
            UserController controller = new UserController(mock.Object);
            User accToDelete = mock.Object.Users.First();
            //act
            var result = controller.Delete(accToDelete.Id);
            //arrange
            mock.Verify(m => m.DeleteUser(accToDelete.Id));
            result.Should().BeOfType<ViewResult>();
            ((ViewResult)result).Model.Should().BeOfType<User[]>();
            ((User[])((ViewResult)result).Model)[0].Id.Should().Be(accToDelete.Id);
        }

        private static Mock<IUserProvider> GenerateMockUserProvider()
        {
            //arrange
            Mock<IUserProvider> mock = new Mock<IUserProvider>();
            mock.Setup(m => m.Users).Returns(new User[]
            {
                new User{ Id=1, Name="Marcin", Email = "marcin@homerp.pl" },
                new User{ Id=2, Name="Alfred", Email = "alfred@homerp.pl" }
            });
            return mock;
        }
    }
}
