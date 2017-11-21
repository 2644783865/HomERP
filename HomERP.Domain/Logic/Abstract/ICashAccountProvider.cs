using System;
using System.Collections.Generic;
using System.Text;
using HomERP.Domain.Entity;
using System.Linq;

namespace HomERP.Domain.Logic.Abstract
{
    public interface ICashAccountProvider
    {
        IQueryable<CashAccount> CashAccounts { get; }
        void SaveCashAccount(CashAccount account);
        CashAccount DeleteCashAccount(int accountId);
    }
}
