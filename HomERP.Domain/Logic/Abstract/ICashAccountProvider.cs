using System;
using System.Collections.Generic;
using System.Text;
using HomERP.Domain.Entity;

namespace HomERP.Domain.Logic.Abstract
{
    public interface ICashAccountProvider
    {
        IEnumerable<Account> CashAccounts { get; }
        void SaveCashAccount(Account account);
        Account DeleteCashAccount(int accountId);
    }
}
