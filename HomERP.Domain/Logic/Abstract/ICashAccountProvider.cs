using System;
using System.Collections.Generic;
using System.Text;
using HomERP.Domain.Entity;

namespace HomERP.Domain.Logic.Abstract
{
    public interface ICashAccountProvider
    {
        IEnumerable<CashAccount> CashAccounts { get; }
        void SaveCashAccount(CashAccount account);
        CashAccount DeleteCashAccount(int accountId);
        Family Family { set; }
    }
}
