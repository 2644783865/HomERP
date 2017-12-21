using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomERP.Domain.Entity;
using HomERP.Domain.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

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
        public IQueryable<CashAccount> CashAccounts
        {
            get { return context.CashAccounts.Include(a=>a.Family); }
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
            if (cashAccount==null)
            {
                return;
            }
            if (cashAccount.Family==null)
            {
                return;
            }
            Family family = context.Families.Find(cashAccount.Family.Id);
            cashAccount.Family = family;

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
