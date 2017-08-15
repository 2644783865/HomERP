using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Moq;
using FluentAssertions;

using HomERP.Domain.Entity;
using HomERP.Domain.Repository.Abstract;
using HomERP.Domain.Logic.Abstract;
using HomERP.Domain.Logic;
using HomERP.Domain.Authentication;

namespace HomERP.Domain.Tests.LogicTests
{
    [TestClass]
    public class FamilyProviderTests
    {
        private Family PrepareExampleFamily()
        {
            return new Family
            {
                Name = "Rodzina Kowalskich",
                Description = "Bardzo sympatyczna rodzina Kowalskich"
            };
        }

        [TestMethod]
        public void Should_Get_All_Families()
        {
            //arrange
            Mock<IFamilyRepository> mock = this.PrepareMockFamilyRepository();
            FamilyProvider provider = new FamilyProvider(mock.Object);

            //act
            IEnumerable<Family> Families = provider.Families;

            //assert
            Families.Should().HaveCount(2, "you have 2 entities in the repository");
        }

        [TestMethod]
        public void Should_Call_FamilyRepository_SaveFamily()
        {
            //arrange
            Family Family = PrepareExampleFamily();
            Mock<IFamilyRepository> mock = this.PrepareMockFamilyRepository();
            FamilyProvider provider = new FamilyProvider(mock.Object);

            //act
            provider.SaveFamily(Family);

            //assert
            //In this place we have to focus on that underlying repository method has been properly called
            //instead of wandering if entity has been properly saved - this is the repository responsibility.
            mock.Verify(m => m.SaveFamily(Family));
        }

        [TestMethod]
        public void Should_Call_FamilyRepository_DeleteUser()
        {
            //arrange
            Mock<IFamilyRepository> mock = this.PrepareMockFamilyRepository();
            Family familyToDelete = mock.Object.Families.Skip(1).First();
            FamilyProvider provider = new FamilyProvider(mock.Object);

            //act
            provider.DeleteFamily(familyToDelete.Id);

            //assert if repository delete method has been called with proper identifier
            mock.Verify(m => m.DeleteFamily(familyToDelete.Id));
        }

        [TestMethod]
        public void Should_Call_FamilyRepository_FamilyForUser()
        {
            //arrange
            Mock<IFamilyRepository> mock = this.PrepareMockFamilyRepository();
            FamilyProvider provider = new FamilyProvider(mock.Object);
            ApplicationUser user = new ApplicationUser { Id = "1", UserName = "User", Email = "user@homerp.pl", Family = provider.Families.First() };

            //act
            Family userFamily = provider.FamilyForUser(user);

            //assert
            mock.Verify(m => m.FamilyForUser(user));
        }

        private Mock<IFamilyRepository> PrepareMockFamilyRepository()
        {
            Mock<IFamilyRepository> mock = new Mock<IFamilyRepository>();
            mock.Setup(m => m.Families).Returns(new Family[]
            {
                new Family { Name = "Rodzina Kowalskich", Description = "" },
                new Family { Name = "Rodzina Iksińskich", Description = "Jeszcze bardziej sympatyczna rodzina Iksińskich" }
            });
            return mock;
        }
    }
}
