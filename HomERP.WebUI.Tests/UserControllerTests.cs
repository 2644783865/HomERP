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
    public class FamilyUserControllerTests
    {
        [TestMethod]
        public void Should_Call_FamilyUserProvider_When_Showing_Users()
        {
            Mock<IFamilyUserProvider> mock = GenerateMockFamilyUserProvider();
            FamilyUserController controller = new FamilyUserController(mock.Object);
            //act
            var result = controller.Index();
            //assert
            mock.VerifyGet(m => m.FamilyUsers);
            result.Should().BeOfType<ViewResult>();
        }

        [TestMethod]
        public void Should_Get_Exact_FamilyUser_When_Edit()
        {
            //arrange
            Mock<IFamilyUserProvider> mock = GenerateMockFamilyUserProvider();
            FamilyUserController controller = new FamilyUserController(mock.Object);
            //act
            var result = controller.Edit(2);
            //assert
            mock.Verify(m => m.FamilyUsers);
            result.Should().BeOfType<ViewResult>();
            ((ViewResult)result).Model.Should().BeOfType<FamilyUser>();
            ((FamilyUser)((ViewResult)result).Model).Name.Should().Be("Alfred");
        }

        [TestMethod]
        public void Should_Modify_FamilyUser_When_Correct_User_Given()
        {
            //arrange
            Mock<IFamilyUserProvider> mock = GenerateMockFamilyUserProvider();
            FamilyUserController controller = new FamilyUserController(mock.Object);
            FamilyUser accToEdit = mock.Object.FamilyUsers.First();
            accToEdit.Name = "Anzelm";
            //act
            var result = controller.Edit(accToEdit);
            //assert
            mock.Verify(m => m.SaveFamilyUser(accToEdit));
            result.Should().BeOfType<ViewResult>();
            ((ViewResult)result).Model.Should().BeOfType<FamilyUser[]>();
        }

        [TestMethod]
        public void Should_Return_Deleted_FamilyUser()
        {
            //arrange
            Mock<IFamilyUserProvider> mock = GenerateMockFamilyUserProvider();
            mock.Setup(m => m.DeleteFamilyUser(1)).Returns(mock.Object.FamilyUsers.First());
            FamilyUserController controller = new FamilyUserController(mock.Object);
            FamilyUser accToDelete = mock.Object.FamilyUsers.First();
            //act
            var result = controller.Delete(accToDelete.Id);
            //arrange
            mock.Verify(m => m.DeleteFamilyUser(accToDelete.Id));
            result.Should().BeOfType<ViewResult>();
            ((ViewResult)result).Model.Should().BeOfType<FamilyUser[]>();
            ((FamilyUser[])((ViewResult)result).Model)[0].Id.Should().Be(accToDelete.Id);
        }

        private static Mock<IFamilyUserProvider> GenerateMockFamilyUserProvider()
        {
            //arrange
            Mock<IFamilyUserProvider> mock = new Mock<IFamilyUserProvider>();
            mock.Setup(m => m.FamilyUsers).Returns(new FamilyUser[]
            {
                new FamilyUser{ Id=1, Name="Marcin", Email = "marcin@homerp.pl" },
                new FamilyUser{ Id=2, Name="Alfred", Email = "alfred@homerp.pl" }
            });
            return mock;
        }
    }
}
