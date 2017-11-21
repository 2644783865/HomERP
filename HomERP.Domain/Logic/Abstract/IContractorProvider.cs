using HomERP.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomERP.Domain.Logic.Abstract
{
    public interface IContractorProvider
    {
        IQueryable<Contractor> Contractors { get; }
        Task<bool> SaveContractorAsync(Contractor contractor);
        Task<bool> DeleteContractorAsync(int contractorId);
    }
}
