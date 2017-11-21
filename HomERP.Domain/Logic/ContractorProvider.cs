using HomERP.Domain.Entity;
using HomERP.Domain.Logic.Abstract;
using HomERP.Domain.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomERP.Domain.Logic
{
    public class ContractorProvider: IContractorProvider
    {
        private IContractorRepository repository;
        private ISessionDataProvider sessionProvider;
        private Family family;
        public ContractorProvider(IContractorRepository repository, ISessionDataProvider sessionProvider)
        {
            this.repository = repository;
            this.sessionProvider = sessionProvider;
            this.family = sessionProvider.Family;
        }

        public IQueryable<Contractor> Contractors
        {
            get
            {
                return repository.Contractors.Where(ca => ca.Family.Id == this.family.Id);
            }
        }

        public async Task<bool> DeleteContractorAsync(int contractorId)
        {
            Contractor contractorToDelete = repository.Contractors.Where(x => x.Id == contractorId).FirstOrDefault();
            if (contractorToDelete == null)
            { return false; }
            if (contractorToDelete.FamilyId != this.family.Id)
            { return false; }

            if (contractorToDelete.Payments.Count>0)
            { return false; }
            var result = await repository.DeleteContractorAsync(contractorId);
            return result;
        }

        public async Task<bool> SaveContractorAsync(Contractor contractor)
        {
            if (contractor.Id == 0) contractor.Family = this.family;
            if (contractor.Family.Id == this.family.Id)
            {
                return await repository.SaveContractorAsync(contractor);
            }
            else
            {
                return false;
            }
        }

    }
}
