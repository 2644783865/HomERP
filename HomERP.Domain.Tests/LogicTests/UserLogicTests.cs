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
    public class FamilyUserLogicTests
    {
        private FamilyUser PrepareExampleFamilyUser()
        {
            return new FamilyUser
            {
                Name = "Marcin",
                PasswordHash="1234",
                Email = "marcin@homerp.pl"
            };
        }

        [TestMethod]
        public void Should_Get_All_FamilyUsers()
        {
            //arrange
            Mock<IFamilyUserRepository> mock = new Mock<IFamilyUserRepository>();
            mock.Setup(m => m.FamilyUsers).Returns(new FamilyUser[]
            {
                new FamilyUser { Name = "Marcin", PasswordHash="1234", Email = "marcin@homerp.pl" },
                new FamilyUser { Name = "Ksawery", PasswordHash="5678", Email = "xawery@homerp.pl" }
            }
            );
            FamilyUserProvider provider = new FamilyUserProvider(mock.Object);

            //act
            IEnumerable<FamilyUser> FamilyUsers = provider.FamilyUsers;

            //assert
            FamilyUsers.Should().HaveCount(2, "you have 2 entities in the repository");
        }

        [TestMethod]
        public void Should_Call_FamilyUserProvider_SaveFamilyUser()
        {
            //arrange
            FamilyUser FamilyUser = PrepareExampleFamilyUser();
            Mock<IFamilyUserRepository> mock = new Mock<IFamilyUserRepository>();
            mock.Setup(m => m.FamilyUsers).Returns(new FamilyUser[]
            {
                new FamilyUser { Name = "Marcin", PasswordHash="1234", Email = "marcin@homerp.pl" },
                new FamilyUser { Name = "Ksawery", PasswordHash="5678", Email = "xawery@homerp.pl" }
            }
                );
            FamilyUserProvider provider = new FamilyUserProvider(mock.Object);

            //act
            provider.SaveFamilyUser(FamilyUser);

            //assert
            //In this place we have to focus on that underlying repository method has been properly called
            //instead of wandering if entity has been properly saved - this is the repository responsibility.
            mock.Verify(m => m.SaveFamilyUser(FamilyUser));
        }

        [TestMethod]
        public void Should_Call_FamilyUserProvider_DeleteUser()
        {
            //arrange
            Mock<IFamilyUserRepository> mock = new Mock<IFamilyUserRepository>();
            mock.Setup(m => m.FamilyUsers).Returns(new FamilyUser[]
            {
                new FamilyUser { Id = 1, Name = "Marcin", PasswordHash="1234", Email = "marcin@homerp.pl" },
                new FamilyUser { Id = 2, Name = "Ksawery", PasswordHash="5678", Email = "xawery@homerp.pl" }
            }
                );
            FamilyUser userToDelete = mock.Object.FamilyUsers.Where(p => p.Id == 2).First();
            FamilyUserProvider provider = new FamilyUserProvider(mock.Object);
 
            //act
            provider.DeleteFamilyUser(userToDelete.Id);

            //assert if repository delete method has been called with proper identifier
            mock.Verify(m => m.DeleteFamilyUser(2));
        }
    }
}
