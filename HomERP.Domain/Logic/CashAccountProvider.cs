using System;
using System.Collections.Generic;
using System.Text;
using HomERP.Domain.Entity;
using HomERP.Domain.Logic.Abstract;
using HomERP.Domain.Repository.Abstract;

namespace HomERP.Domain.Logic
{
    public class CashAccountProvider : ICashAccountProvider
    {
        private ICashAccountRepository repository;
        public CashAccountProvider(ICashAccountRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<CashAccount> CashAccounts
        { get { return repository.CashAccounts; } }

        public CashAccount DeleteCashAccount(int cashAccountId)
        {
            return repository.DeleteCashAccount(cashAccountId);
        }

        public void SaveCashAccount(CashAccount cashAccount)
        {
            repository.SaveCashAccount(cashAccount);
        }
    }
}
