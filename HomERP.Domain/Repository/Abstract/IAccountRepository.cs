using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HomERP.Domain.Entity;

namespace HomERP.Domain.Repository.Abstract
{
    public interface IAccountRepository
    {
        IEnumerable<Account> Accounts { get; }
        void SaveAccount(Account account);
        Account DeleteAccount(int accountId);
    }
}
