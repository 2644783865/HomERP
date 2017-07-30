using System;
using System.Collections.Generic;
using System.Text;
using HomERP.Domain.Entity;
using HomERP.Domain.Logic.Abstract;
using HomERP.Domain.Repository.Abstract;

namespace HomERP.Domain.Logic
{
    public class AccountProvider : IAccountProvider
    {
        private IAccountRepository repository;
        public AccountProvider(IAccountRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<Account> Accounts
        { get { return repository.Accounts; } }

        public Account DeleteAccount(int accountId)
        {
            return repository.DeleteAccount(accountId);
        }

        public void SaveAccount(Account account)
        {
            repository.SaveAccount(account);
        }
    }
}
