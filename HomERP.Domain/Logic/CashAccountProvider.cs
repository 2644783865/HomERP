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
        private IAccountRepository repository;
        public CashAccountProvider(IAccountRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<Account> CashAccounts
        { get { return repository.Accounts; } }

        public Account DeleteCashAccount(int accountId)
        {
            return repository.DeleteAccount(accountId);
        }

        public void SaveCashAccount(Account account)
        {
            repository.SaveAccount(account);
        }
    }
}
