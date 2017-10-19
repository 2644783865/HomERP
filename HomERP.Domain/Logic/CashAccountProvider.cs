using System;
using System.Collections.Generic;
using System.Text;
using HomERP.Domain.Entity;
using HomERP.Domain.Logic.Abstract;
using HomERP.Domain.Repository.Abstract;
using System.Linq;
using HomERP.Domain.Authentication;

namespace HomERP.Domain.Logic
{
    public class CashAccountProvider : ICashAccountProvider
    {
        private ICashAccountRepository repository;
        private ISessionDataProvider sessionProvider;
        private Family family;
        public CashAccountProvider(ICashAccountRepository repository, ISessionDataProvider sessionProvider)
        {
            this.repository = repository;
            this.sessionProvider = sessionProvider;
            this.family = sessionProvider.Family;
        }

        public IEnumerable<CashAccount> CashAccounts
        {
            get
            {
                if (this.family == null) throw new ArgumentNullException("CashAccount requires an user to have a family.");
                return repository.CashAccounts.Where(ca => ca.Family.Id == this.family.Id);
            }
        }

        public CashAccount DeleteCashAccount(int cashAccountId)
        {
            var cashAccountToDelete = repository.CashAccounts.Where(ca => ca.Id == cashAccountId && ca.Family.Id == this.family.Id).FirstOrDefault();
            if (cashAccountToDelete.Id == cashAccountId)
            {
                return repository.DeleteCashAccount(cashAccountId);
            }
            return null;
        }

        public void SaveCashAccount(CashAccount cashAccount)
        {
            if (cashAccount.Id == 0) cashAccount.Family = this.family;
            if (cashAccount.Family.Id == this.family.Id)
            {
                repository.SaveCashAccount(cashAccount);
            }
        }
    }
}
