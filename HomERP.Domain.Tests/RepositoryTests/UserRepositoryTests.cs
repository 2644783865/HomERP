using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using FluentAssertions;
using HomERP.Domain.Repository.EntityFramework;
using HomERP.Domain.Repository.Abstract;
using HomERP.Domain.Entity;

namespace HomERP.Domain.Tests.RepositoryTests
{
    [TestClass]
    public class FamilyUserRepositoryTests
    {
        private EfDbContext context;
        private IFamilyUserRepository repository;

        [TestInitialize]
        public void Initialize()
        {
            context = new EfDbContext(HomERP.Domain.Tests.Context.MemoryContext.GenerateContextOptions());
            repository = new EfFamilyUserRepository(context);
        }
        [TestMethod]
        public void Should_Add_FamilyUser_To_Context_When_Saving_Repository()
        {
            //arrange
            FamilyUser user = new FamilyUser() { Name = "Zenon", Email= "zenon@homerp.pl"};
            //act
            repository.SaveFamilyUser(user);
            //assert
            //check if object has been written to the context
            context.FamilyUsers.Count().Should().Be(1, "when we add one object to empty collection, there should be only this one.");
            var resultUser = context.FamilyUsers.FirstOrDefault();
            resultUser.Name.Should().Be(user.Name);
            resultUser.Email.ToString().Should().Be(user.Email.ToString());
        }

        [TestMethod]
        public void Should_Update_Context_When_Updating_FamilyUser()
        {
            //arrange
            FamilyUser user = new FamilyUser() { Name = "Zenon", Email = "zenon@homerp.pl" };
            repository.SaveFamilyUser(user);
            FamilyUser testUser = repository.FamilyUsers.Where(a => a.Name == "Zenon").First();
            testUser.Name = "Franek";
            testUser.Email = "franek@homerp.pl";
            //act
            repository.SaveFamilyUser(testUser);
            //assert
            context.FamilyUsers.Count().Should().Be(1);
            FamilyUser resultUser = context.FamilyUsers.Where(a => a.Name == testUser.Name).First();
            resultUser.Name.Should().Be(testUser.Name);
            resultUser.Email.ToString().Should().Be(testUser.Email.ToString());
        }

        [TestMethod]
        public void Should_Return_Deleted_FamilyUser_When_Deleting_From_Repository()
        {
            //arrange
            FamilyUser user = new FamilyUser() { Name = "Zenon", Email = "zenon@homerp.pl" };
            repository.SaveFamilyUser(user);
            //act
            int id = context.FamilyUsers.First().Id;
            FamilyUser deletedUser = repository.DeleteFamilyUser(id);
            //assert
            id.Should().NotBe(0, "i already added a FamilyUser to repository, so it should be written to context.");
            deletedUser.Id.Should().Be(id);
            deletedUser.Name.Should().Be(user.Name);
            context.FamilyUsers.Count().Should().Be(0);
        }
    }
}
