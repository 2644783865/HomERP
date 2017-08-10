using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using HomERP.Domain.Entity;
using HomERP.Domain.Repository.Abstract;
using HomERP.Domain.Repository.EntityFramework;

using Moq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomERP.Domain.Tests.RepositoryTests
{
    [TestClass]
    public class FamilyRepositoryTests
    {
        private EfDbContext context;
        private IFamilyRepository repository;

        [TestInitialize]
        public void Initialize()
        {
            context = new EfDbContext(HomERP.Domain.Tests.Context.MemoryContext.GenerateContextOptions());
            repository = new EfFamilyRepository(context);
        }

        private Family PrepareExampleFamily()
        {
            return new Family { Name = "Rodzinka", Description = "Sympatyczna rodzinka Kowalskich" };
        }

        [TestMethod]
        public void Should_Add_Family_To_Context_When_Saving_New_Family()
        {
            //arrange
            Family fam = PrepareExampleFamily();
            //act
            repository.SaveFamily(fam);
            //assert
            repository.Families.Should().HaveCount(1);
        }

        [TestMethod]
        public void Should_Update_Context_When_Saving_Existing_Family()
        {
            //arrange
            Family fam = PrepareExampleFamily();
            repository.SaveFamily(fam);
            Family famToUpdate = context.Families.First();
            //act
            famToUpdate.Name = "Rodzina Iksińskich";
            famToUpdate.Description = "Równie sympatyczna rodzinka Iksińskich";
            repository.SaveFamily(famToUpdate);
            //assert
            context.Families.Should().HaveCount(1);
            Family famUpdated = context.Families.First();
            famUpdated.Name.Should().Be(famToUpdate.Name);
            famUpdated.Description.Should().Be(famToUpdate.Description);
        }
        [TestMethod]
        public void Should_Remove_Family_When_Deleting_Existing_Family()
        {
            //arrange
            Family fam = PrepareExampleFamily();
            repository.SaveFamily(fam);
            Family famToDelete = context.Families.First();
            //act
            repository.DeleteFamily(famToDelete.Id);
            //assert
            context.Families.Should().BeEmpty();
        }
    }
}
