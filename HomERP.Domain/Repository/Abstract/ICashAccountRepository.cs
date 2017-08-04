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
        IEnumerable<Account> CashAccounts { get; }
        void SaveCashAccount(Account cashAccount);
        Account DeleteCashAccount(int cashAccountId);
    }
}
