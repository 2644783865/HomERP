using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using FluentAssertions;
using HomERP.Domain.Repository.Abstract;
using HomERP.Domain.Entity;
using HomERP.Domain.Logic.Abstract;
using HomERP.Domain.Logic;
using System.Linq;
using System.Threading.Tasks;

namespace HomERP.Domain.Tests.LogicTests
{
    [TestClass]
    public class ContractorLogicTests
    {
        [TestMethod]
        public void ContractorProvider_Should_Return_Only_Family_Contractors()
        {
            //arrange
            Mock<IContractorRepository> mock = new Mock<IContractorRepository>();
            mock.Setup(x => x.Contractors).Returns(new Contractor[]
            {
                new Contractor {Id=1, ShortName = "Firma1", FamilyId = 1, Family = new Family { Id = 1 } },
                new Contractor {Id=2, ShortName = "Firma2", FamilyId = 1, Family = new Family { Id = 1 } },
                new Contractor {Id=3, ShortName = "Firma3", FamilyId = 2, Family = new Family { Id = 2 } },
            });
            Mock<ISessionDataProvider> sessionProvider = new Mock<ISessionDataProvider>();
            sessionProvider.Setup(s => s.Family).Returns(new Family { Id = 1, Name = "Super Family" });
            IContractorProvider provider = new ContractorProvider(mock.Object, sessionProvider.Object);
            //act
            IEnumerable<Contractor> contractors = provider.Contractors;
            //assert
            contractors.Should().HaveCount(2, "one contractor belongs to other family");
        }

        [TestMethod]
        public async Task ContractorProvider_Should_Save_New_Contractor_To_Own_Family()
        {
            //arrange
            Mock<IContractorRepository> mock = new Mock<IContractorRepository>();
            mock.Setup(x => x.SaveContractorAsync(It.IsAny<Contractor>())).Returns(Task.FromResult<bool>(true));
            IContractorRepository mockRepository = mock.Object;
            Mock<ISessionDataProvider> sessionProvider = new Mock<ISessionDataProvider>();
            sessionProvider.Setup(s => s.Family).Returns(new Family { Id = 1, Name = "Super Family" });
            Contractor newContractor = new Contractor { Id = 0, ShortName = "Firma1", FamilyId = 0 };
            IContractorProvider provider = new ContractorProvider(mockRepository, sessionProvider.Object);
            //act
            var result = await provider.SaveContractorAsync(newContractor);
            //assert
            result.Should().BeTrue();
            mock.Verify(x => x.SaveContractorAsync(It.IsAny<Contractor>()), Times.Once);
        }

        [TestMethod]
        public async Task ContractorProvider_Should_Save_Existing_Contractor_Of_Own_Family()
        {
            //arrange
            Mock<IContractorRepository> mock = new Mock<IContractorRepository>();
            mock.Setup(x => x.SaveContractorAsync(It.IsAny<Contractor>())).Returns(Task.FromResult<bool>(true));
            IContractorRepository mockRepository = mock.Object;
            Mock<ISessionDataProvider> sessionProvider = new Mock<ISessionDataProvider>();
            sessionProvider.Setup(s => s.Family).Returns(new Family { Id = 1, Name = "Super Family" });
            Contractor newContractor = new Contractor { Id = 1, ShortName = "Firma1", Family = new Family { Id = 1, Name = "Super Family" } };
            IContractorProvider provider = new ContractorProvider(mockRepository, sessionProvider.Object);
            //act
            var result = await provider.SaveContractorAsync(newContractor);
            //assert
            result.Should().BeTrue();
            mock.Verify(x => x.SaveContractorAsync(It.IsAny<Contractor>()), Times.Once);
        }

        [TestMethod]
        public async Task ContractorProvider_Should_Not_Save_Contractor_Of_Other_Family()
        {
            //arrange
            Mock<IContractorRepository> mock = new Mock<IContractorRepository>();
            mock.Setup(x => x.SaveContractorAsync(It.IsAny<Contractor>())).Returns(Task.FromResult<bool>(true));
            IContractorRepository mockRepository = mock.Object;
            Mock<ISessionDataProvider> sessionProvider = new Mock<ISessionDataProvider>();
            sessionProvider.Setup(s => s.Family).Returns(new Family { Id = 1, Name = "Super Family" });
            Contractor newContractor = new Contractor { Id = 3, ShortName = "Firma1", Family = new Family { Id = 2, Name = "Other Family" } };
            IContractorProvider provider = new ContractorProvider(mockRepository, sessionProvider.Object);
            //act
            var result = await provider.SaveContractorAsync(newContractor);
            //assert
            result.Should().BeFalse();
            mock.Verify(x => x.SaveContractorAsync(It.IsAny<Contractor>()), Times.Never);
        }

        [TestMethod]
        public async Task ContractorProvider_Should_Delete_Contractor_Of_Own_Family()
        {
            //arrange
            Mock<IContractorRepository> mock = new Mock<IContractorRepository>();
            mock.Setup(x => x.Contractors).Returns(new Contractor[]
            {
                new Contractor {Id=1, ShortName = "Firma1", FamilyId = 1, Family = new Family { Id = 1 } },
                new Contractor {Id=2, ShortName = "Firma2", FamilyId = 1, Family = new Family { Id = 1 } },
                new Contractor {Id=3, ShortName = "Firma3", FamilyId = 2, Family = new Family { Id = 2 } },
            });
            mock.Setup(x => x.DeleteContractorAsync(It.IsAny<int>())).Returns(Task.FromResult<bool>(true));
            IContractorRepository mockRepository = mock.Object;
            Contractor contractorToDelete = new Contractor { Id = 1, ShortName = "Firma1" };
            Mock<ISessionDataProvider> sessionProvider = new Mock<ISessionDataProvider>();
            sessionProvider.Setup(s => s.Family).Returns(new Family { Id = 1, Name = "Super Family" });
            IContractorProvider provider = new ContractorProvider(mockRepository, sessionProvider.Object);
            //act
            var result = await provider.DeleteContractorAsync(contractorToDelete.Id);
            //assert
            result.Should().BeTrue();
            mock.Verify(x => x.DeleteContractorAsync(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public async Task ContractorProvider_Should_Not_Delete_Contractor_Of_Other_Family()
        {
            //arrange
            Mock<IContractorRepository> mock = new Mock<IContractorRepository>();
            mock.Setup(x => x.Contractors).Returns(new Contractor[]
            {
                new Contractor {Id=1, ShortName = "Firma1", FamilyId = 1, Family = new Family { Id = 1 } },
                new Contractor {Id=2, ShortName = "Firma2", FamilyId = 1, Family = new Family { Id = 1 } },
                new Contractor {Id=3, ShortName = "Firma3", FamilyId = 2, Family = new Family { Id = 2 } },
            });
            mock.Setup(x => x.DeleteContractorAsync(It.IsAny<int>())).Returns(Task.FromResult<bool>(true));
            IContractorRepository mockRepository = mock.Object;
            Contractor contractorToDelete = new Contractor { Id = 3, ShortName = "Firma1" };
            Mock<ISessionDataProvider> sessionProvider = new Mock<ISessionDataProvider>();
            sessionProvider.Setup(s => s.Family).Returns(new Family { Id = 1, Name = "Super Family" });
            IContractorProvider provider = new ContractorProvider(mockRepository, sessionProvider.Object);
            //act
            var result = await provider.DeleteContractorAsync(contractorToDelete.Id);
            //assert
            result.Should().BeFalse();
            mock.Verify(x => x.DeleteContractorAsync(It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        public async Task ContractorProvider_Should_Not_Delete_Contractor_When_Has_Related_Entries()
        {
            //arrange
            Mock<IContractorRepository> mock = new Mock<IContractorRepository>();
            mock.Setup(x => x.Contractors).Returns(new Contractor[]
            {
                new Contractor {Id=1, ShortName = "Firma1", FamilyId = 1, Family = new Family { Id = 1 },
                    Payments = new Payment[]
                    {
                        new Payment {Id = 1}
                    }
                },
                new Contractor {Id=2, ShortName = "Firma2", FamilyId = 1, Family = new Family { Id = 1 } },
                new Contractor {Id=3, ShortName = "Firma3", FamilyId = 1, Family = new Family { Id = 2 } },
            });
            mock.Setup(x => x.DeleteContractorAsync(It.IsAny<int>())).Returns(Task.FromResult<bool>(true));
            IContractorRepository mockRepository = mock.Object;
            Contractor contractorToDelete = new Contractor { Id = 1, ShortName = "Firma1" };
            Mock<ISessionDataProvider> sessionProvider = new Mock<ISessionDataProvider>();
            sessionProvider.Setup(s => s.Family).Returns(new Family { Id = 1, Name = "Super Family" });
            IContractorProvider provider = new ContractorProvider(mockRepository, sessionProvider.Object);
            //act
            var result = await provider.DeleteContractorAsync(contractorToDelete.Id);
            //assert
            result.Should().BeFalse();
            mock.Verify(x => x.DeleteContractorAsync(It.IsAny<int>()), Times.Never);
        }
    }
}