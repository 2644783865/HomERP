using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using HomERP.Domain.Authentication;
using System.Threading.Tasks;
using HomERP.Domain.Logic.Abstract;
using HomERP.Domain.Entity;
using HomERP.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace HomERP.WebUI.Tests
{
    [TestClass]
    public class FamilyControllerTests
    {
        [TestMethod]
        public void Should_Return_View_With_Family_Details()
        {
            //arrange
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            Mock<UserManager<ApplicationUser>> mockUserManager = new Mock<UserManager<ApplicationUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);
            mockUserManager.Setup(m => m.GetUserAsync(It.IsAny<System.Security.Claims.ClaimsPrincipal>()))
                .Returns(Task.FromResult<ApplicationUser>(
                    new ApplicationUser { UserName = "User", Email = "user@homerp.pl", Family = null }
                ));
            Mock<IFamilyProvider> mockFamilyProvider = new Mock<IFamilyProvider>();
            mockFamilyProvider.Setup(m => m.FamilyForUser(It.IsAny<ApplicationUser>()))
                .Returns( new Family { Id = 1, Name = "Rodzinka" });
            Mock<IUserProvider> mockUserProvider = new Mock<IUserProvider>();
            FamilyController controller = new FamilyController(mockFamilyProvider.Object, mockUserProvider.Object, mockUserManager.Object);
            //act
            var result = controller.Index();
            //assert
            result.Should().BeOfType<Task<IActionResult>>();
            mockFamilyProvider.Verify(x => x.FamilyForUser(It.IsAny<ApplicationUser>()));
        }
    }
}
