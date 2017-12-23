using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HomERP.Domain.Entity;

namespace HomERP.Domain.Repository.Abstract
{
    public interface ICashAccountRepository
    {
        IQueryable<CashAccount> CashAccounts { get; }
        Task<bool> SaveCashAccountAsync(CashAccount cashAccount);
        Task<int> DeleteRangeAsync(System.Linq.Expressions.Expression<Func<CashAccount, bool>> predicate);
    }
}
