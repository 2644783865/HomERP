using System;
using System.Collections.Generic;
using System.Text;
using HomERP.Domain.Entity;
using HomERP.Domain.Logic.Abstract;
using HomERP.Domain.Repository.Abstract;
using System.Linq;
using HomERP.Domain.Authentication;
using System.Threading.Tasks;

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

        public IQueryable<CashAccount> CashAccounts
        {
            get
            {
                return repository.CashAccounts.Where(ca => ca.Family.Id == this.family.Id);
            }
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<int> identifiers)
        {
            int result = await repository.DeleteRangeAsync(c => identifiers.Contains(c.Id) && c.Family.Id == family.Id);
            return result == identifiers.Count();
        }

        public async Task<bool> SaveCashAccountAsync(CashAccount cashAccount)
        {
            if (cashAccount.Id == 0) cashAccount.Family = this.family;
            if (cashAccount.Family.Id == this.family.Id)
            {
                return await repository.SaveCashAccountAsync(cashAccount);
            }
            return false;
        }
    }
}
