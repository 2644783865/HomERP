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
            get { return context.CashAccounts; }
        }

        public CashAccount DeleteCashAccount(int cashAccountId)
        {
            CashAccount acc = context.CashAccounts.Find(cashAccountId);
            if (acc!=null)
            {
                context.CashAccounts.Remove(acc);
                context.SaveChanges();
            }
            return acc;
        }

        public void SaveCashAccount(CashAccount cashAccount)
        {
            if (cashAccount.Id==0)
            {
                context.CashAccounts.Add(cashAccount);
            }
            else
            {
                CashAccount cashAccountToUpdate = context.CashAccounts.Find(cashAccount.Id);
                if (cashAccountToUpdate!=null)
                {
                    cashAccountToUpdate.InitialAmount = cashAccount.InitialAmount;
                    cashAccountToUpdate.Name = cashAccount.Name;
                    cashAccountToUpdate.Family = cashAccount.Family;
                }
            }
            context.SaveChanges();
        }
    }
}
