using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

using Moq;
using FluentAssertions;
using HomERP.Domain.Repository.EntityFramework;
using HomERP.Domain.Repository.Abstract;
using HomERP.Domain.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace HomERP.Domain.Tests.RepositoryTests
{
    [TestClass]
    public class ContractorRepositoryTests
    {
        private EfDbContext context;
        private IContractorRepository repository;

        [TestInitialize]
        public void Initialize()
        {
            context = new EfDbContext(HomERP.Domain.Tests.Context.MemoryContext.GenerateContextOptions());
            repository = new EfContractorRepository(context);
        }

        [TestMethod]
        public void ContractorRepository_Should_Return_Contractors_From_Context()
        {
            //arrange
            Contractor contractor = new Contractor
            {
                BuildingNumber = "10",
                City = "Rzeszów",
                Name = "Firma",
                ShortName = "Firma"
            };
            context.Contractors.Add(contractor);
            context.SaveChanges();
            //act
            IEnumerable<Contractor> contractors = repository.Contractors;
            //assert
            contractors.Should().HaveCount(1);
        }

        [TestMethod]
        public async Task ContractorRepository_Should_Add_New_Contractor_To_Context()
        {
            //arrange
            Contractor contractor = CreateSampleContractor();
            //act
            var result = await repository.SaveContractorAsync(contractor);
            //assert
            result.Should().BeTrue();
            context.Contractors.Should().HaveCount(1);
            context.Contractors.First().ShortName.Should().Be(contractor.ShortName);
        }

        [TestMethod]
        public async Task ContractorRepository_Should_Update_Existing_Contractor_In_Context()
        {
            //arrange
            Contractor contractor = CreateSampleContractor();
            context.Contractors.Add(contractor);
            context.SaveChanges();
            Contractor contractorToChange = context.Contractors.First();

            contractorToChange.BuildingNumber = "2";
            contractorToChange.City = "City2";
            contractorToChange.Description = "Desc2";
            contractorToChange.Email = "Email2";
            contractorToChange.Enabled = false;
            contractorToChange.LocalNumber = "L2";
            contractorToChange.Name = "Name2";
            contractorToChange.NIP = "NIP2";
            contractorToChange.Phone = "Phone2";
            contractorToChange.PostalCode = "PostalCode2";
            contractorToChange.ShortName = "ShortName2";
            contractorToChange.Street = "Street2";
            contractorToChange.Url = "Url2";
            //act
            var result = await repository.SaveContractorAsync(contractorToChange);
            //assert
            result.Should().BeTrue();
            context.Contractors.Should()./*Still*/HaveCount(1);
            Contractor modifiedContractor = context.Contractors.First();
            modifiedContractor.BuildingNumber.Should().Be(contractorToChange.BuildingNumber);
            modifiedContractor.City.Should().Be(contractorToChange.City);
            modifiedContractor.Description.Should().Be(contractorToChange.Description);
            modifiedContractor.Email.Should().Be(contractorToChange.Email);
            modifiedContractor.Enabled.Should().Be(contractorToChange.Enabled);
            modifiedContractor.LocalNumber.Should().Be(contractorToChange.LocalNumber);
            modifiedContractor.Name.Should().Be(contractorToChange.Name);
            modifiedContractor.NIP.Should().Be(contractorToChange.NIP);
            modifiedContractor.Phone.Should().Be(contractorToChange.Phone);
            modifiedContractor.PostalCode.Should().Be(contractorToChange.PostalCode);
            modifiedContractor.ShortName.Should().Be(contractorToChange.ShortName);
            modifiedContractor.Street.Should().Be(contractorToChange.Street);
            modifiedContractor.Url.Should().Be(contractorToChange.Url);
        }

        [TestMethod]
        public async Task ContractorRepository_Should_Delete_Existing_Contractor_In_Context()
        {
            //arrange
            Contractor contractor = CreateSampleContractor();
            context.Contractors.Add(contractor);
            context.SaveChanges();
            Contractor contractorToDelete = context.Contractors.First();
            //act
            var result = await repository.DeleteContractorAsync(contractorToDelete.Id);
            //assert
            contractorToDelete.Should().NotBeNull();
            contractorToDelete.Id.Should().BeGreaterThan(0);
            result.Should().BeTrue();
            context.Contractors.Should().HaveCount(0);
        }

        private Contractor CreateSampleContractor()
        {
            return new Contractor
            {
                BuildingNumber = "1",
                City = "City1",
                Description = "Desc1",
                Email = "Email1",
                Enabled = true,
                LocalNumber = "L1",
                Name = "Name1",
                NIP = "NIP1",
                Phone = "Phone1",
                PostalCode = "PostalCode1",
                ShortName = "ShortName1",
                Street = "Street1",
                Url = "Url1"
            };
        }
    }
}
