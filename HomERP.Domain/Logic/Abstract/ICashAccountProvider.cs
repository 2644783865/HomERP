using System;
using System.Collections.Generic;
using System.Text;
using HomERP.Domain.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace HomERP.Domain.Logic.Abstract
{
    public interface ICashAccountProvider
    {
        IQueryable<CashAccount> CashAccounts { get; }
        Task<bool> SaveCashAccountAsync(CashAccount account);
        Task<bool> DeleteRangeAsync(IEnumerable<int> identifiers);
    }
}
