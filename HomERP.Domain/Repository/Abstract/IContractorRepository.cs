using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HomERP.Domain.Entity;

namespace HomERP.Domain.Repository.Abstract
{
    public interface IContractorRepository
    {
        IQueryable<Contractor> Contractors { get; }
        Task<bool> SaveContractorAsync(Contractor contractor);
        Task<bool> DeleteContractorAsync(int contractorId);
    }
}
