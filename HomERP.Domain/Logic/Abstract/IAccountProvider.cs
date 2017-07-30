using System;
using System.Collections.Generic;
using System.Text;
using HomERP.Domain.Entity;

namespace HomERP.Domain.Logic.Abstract
{
    public interface IAccountProvider
    {
        IEnumerable<Account> Accounts { get; }
        void SaveAccount(Account account);
        Account DeleteAccount(int accountId);
    }
}
