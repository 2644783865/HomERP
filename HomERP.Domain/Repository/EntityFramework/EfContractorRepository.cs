using HomERP.Domain.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using HomERP.Domain.Entity;
using System.Threading.Tasks;

namespace HomERP.Domain.Repository.EntityFramework
{
    public class EfContractorRepository : IContractorRepository
    {
        private EfDbContext context;
        public EfContractorRepository(EfDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Contractor> Contractors { get { return context.Contractors; } }

        public async Task<bool> DeleteContractorAsync(int contractorId)
        {
            Contractor contractorToDelete = context.Contractors.Find(contractorId);
            context.Contractors.Remove(contractorToDelete);
            var result = await context.SaveChangesAsync();
            return result == 1;
        }

        public async Task<bool> SaveContractorAsync(Contractor contractor)
        {
            if (contractor.Id == 0)
            {
                context.Contractors.Add(contractor);
            }
            else
            {
                Contractor contractorToUpdate = context.Contractors.Find(contractor.Id);
                contractorToUpdate.BuildingNumber = contractor.BuildingNumber;
                contractorToUpdate.City = contractor.City;
                contractorToUpdate.Description = contractor.Description;
                contractorToUpdate.Email = contractor.Email;
                contractorToUpdate.Enabled = contractor.Enabled;
                contractorToUpdate.LocalNumber = contractor.LocalNumber;
                contractorToUpdate.Name = contractor.Name;
                contractorToUpdate.NIP = contractor.NIP;
                contractorToUpdate.Phone = contractor.Phone;
                contractorToUpdate.PostalCode = contractor.PostalCode;
                contractorToUpdate.ShortName = contractor.ShortName;
                contractorToUpdate.Street = contractor.Street;
                contractorToUpdate.Url = contractor.Url;
            }
            int result = await context.SaveChangesAsync();
            return result==1;
        }
    }
}
