using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomERP.Domain.Entity;
using HomERP.Domain.Repository.Abstract;

namespace HomERP.Domain.Repository.EntityFramework
{
    public class EfCashAccountRepository : ICashAccountRepository
    {
        public EfCashAccountRepository(EfDbContext context)
        {
            this.context = context;
        }
        private EfDbContext context;
//        private EfDbContext context = new EfDbContext();
        public IEnumerable<CashAccount> CashAccounts
        {
            get { return context.Accounts; }
        }

        public CashAccount DeleteCashAccount(int cashAccountId)
        {
            CashAccount acc = context.Accounts.Find(cashAccountId);
            if (acc!=null)
            {
                context.Accounts.Remove(acc);
                context.SaveChanges();
            }
            return acc;
        }

        public void SaveCashAccount(CashAccount cashAccount)
        {
            if (cashAccount.Id==0)
            {
                context.Accounts.Add(cashAccount);
            }
            else
            {
                CashAccount cashAccountToUpdate = context.Accounts.Find(cashAccount.Id);
                if (cashAccountToUpdate!=null)
                {
                    cashAccountToUpdate.InitialAmount = cashAccount.InitialAmount;
                    cashAccountToUpdate.Name = cashAccount.Name;
                }
            }
            context.SaveChanges();
        }
    }
}
