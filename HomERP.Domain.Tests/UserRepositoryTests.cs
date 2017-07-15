using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Effort;
using FluentAssertions;
using HomERP.Domain.Repository.EntityFramework;
using HomERP.Domain.Repository.Abstract;
using HomERP.Domain.Entity;

namespace HomERP.Domain.Tests
{
    [TestClass]
    public class UserRepositoryTests
    {
        private EfDbContext context;
        private IUserRepository repository;

        [TestInitialize]
        public void Initialize()
        {
            var connection = DbConnectionFactory.CreateTransient();
            this.context = new EfDbContext(connection);
            this.repository = new EfUserRepository(this.context);
        }
        [TestMethod]
        public void Test_AddUser()
        {
            //arrange
            User user = new User() { Name = "Zenon", Email= "zenon@homerp.pl"};
            //act
            repository.SaveUser(user);
            //assert
            //check if object has been written to the context
            context.Users.Count().Should().Be(1, "when we add one object to empty collection, there should be only this one.");
            var resultUser = context.Users.FirstOrDefault();
            resultUser.Name.Should().Be(user.Name);
            resultUser.Email.ToString().Should().Be(user.Email.ToString());
        }

        [TestMethod]
        public void Test_EditUser()
        {
            //arrange
            User user = new User() { Name = "Zenon", Email = "zenon@homerp.pl" };
            repository.SaveUser(user);
            User testUser = repository.Users.Where(a => a.Name == "Zenon").First();
            testUser.Name = "Franek";
            testUser.Email = "franek@homerp.pl";
            //act
            repository.SaveUser(testUser);
            //assert
            context.Users.Count().Should().Be(1);
            User resultUser = context.Users.Where(a => a.Name == testUser.Name).First();
            resultUser.Name.Should().Be(testUser.Name);
            resultUser.Email.ToString().Should().Be(testUser.Email.ToString());
        }

        [TestMethod]
        public void Test_DeleteUser()
        {
            //arrange
            User user = new User() { Name = "Zenon", Email = "zenon@homerp.pl" };
            repository.SaveUser(user);
            //act
            int id = context.Users.First().Id;
            User deletedUser = repository.DeleteUser(id);
            //assert
            id.Should().NotBe(0, "i already added a User to repository, so it should be written to context.");
            deletedUser.Id.Should().Be(id);
            deletedUser.Name.Should().Be(user.Name);
            context.Accounts.Count().Should().Be(0);
        }
    }
}
