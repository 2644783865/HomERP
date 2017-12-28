using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public IQueryable<CashAccount> CashAccounts
        {
            get { return context.CashAccounts.Include(a=>a.Family); }
        }

        public async Task<bool> SaveCashAccountAsync(CashAccount cashAccount)
        {
            if (cashAccount==null)
            {
                return false;
            }
            if (cashAccount.Family==null)
            {
                return false;
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
                    if (context.Entry(cashAccountToUpdate).State == EntityState.Unchanged)
                    {
                        return true;
                    }
                }
            }
            int result = await context.SaveChangesAsync();
            return result == 1;
        }

        public async Task<int> DeleteRangeAsync(IEnumerable<int> identifiers)
        {
            context.CashAccounts.RemoveRange(context.CashAccounts.Where(c => identifiers.Contains(c.Id)));
            return await context.SaveChangesAsync();
        }

        
    }
}
