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
    public class UserLogicTests
    {
        private User PrepareExampleUser()
        {
            return new User
            {
                Name = "Marcin",
                PasswordHash="1234",
                Email = "marcin@homerp.pl"
            };
        }

        [TestMethod]
        public void Should_Get_All_Users()
        {
            //arrange
            Mock<IUserRepository> mock = new Mock<IUserRepository>();
            mock.Setup(m => m.Users).Returns(new User[]
            {
                new User { Name = "Marcin", PasswordHash="1234", Email = "marcin@homerp.pl" },
                new User { Name = "Ksawery", PasswordHash="5678", Email = "xawery@homerp.pl" }
            }
            );
            UserProvider provider = new UserProvider(mock.Object);

            //act
            IEnumerable<User> Users = provider.Users;

            //assert
            Users.Should().HaveCount(2, "you have 2 entities in the repository");
        }

        [TestMethod]
        public void Should_Call_UserProvider_SaveUser()
        {
            //arrange
            User User = PrepareExampleUser();
            Mock<IUserRepository> mock = new Mock<IUserRepository>();
            mock.Setup(m => m.Users).Returns(new User[]
            {
                new User { Name = "Marcin", PasswordHash="1234", Email = "marcin@homerp.pl" },
                new User { Name = "Ksawery", PasswordHash="5678", Email = "xawery@homerp.pl" }
            }
                );
            UserProvider provider = new UserProvider(mock.Object);

            //act
            provider.SaveUser(User);

            //assert
            //In this place we have to focus on that underlying repository method has been properly called
            //instead of wandering if entity has been properly saved - this is the repository responsibility.
            mock.Verify(m => m.SaveUser(User));
        }

        [TestMethod]
        public void Should_Call_UserProvider_DeleteUser()
        {
            //arrange
            Mock<IUserRepository> mock = new Mock<IUserRepository>();
            mock.Setup(m => m.Users).Returns(new User[]
            {
                new User { Id = 1, Name = "Marcin", PasswordHash="1234", Email = "marcin@homerp.pl" },
                new User { Id = 2, Name = "Ksawery", PasswordHash="5678", Email = "xawery@homerp.pl" }
            }
                );
            User userToDelete = mock.Object.Users.Where(p => p.Id == 2).First();
            UserProvider provider = new UserProvider(mock.Object);
 
            //act
            provider.DeleteUser(userToDelete.Id);

            //assert if repository delete method has been called with proper identifier
            mock.Verify(m => m.DeleteUser(2));
        }
    }
}
