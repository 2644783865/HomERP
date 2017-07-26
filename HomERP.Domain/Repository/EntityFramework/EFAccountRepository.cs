using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomERP.Domain.Entity;
using HomERP.Domain.Repository.Abstract;

namespace HomERP.Domain.Repository.EntityFramework
{
    public class EfAccountRepository : IAccountRepository
    {
        public EfAccountRepository(EfDbContext context)
        {
            this.context = context;
        }
        private EfDbContext context;
//        private EfDbContext context = new EfDbContext();
        public IEnumerable<Account> Accounts
        {
            get { return context.Accounts; }
        }

        public Account DeleteAccount(int accountId)
        {
            Account acc = context.Accounts.Find(accountId);
            if (acc!=null)
            {
                context.Accounts.Remove(acc);
                context.SaveChanges();
            }
            return acc;
        }

        public void SaveAccount(Account account)
        {
            if (account.Id==0)
            {
                context.Accounts.Add(account);
            }
            else
            {
                Account accountToUpdate = context.Accounts.Find(account.Id);
                if (accountToUpdate!=null)
                {
                    accountToUpdate.InitialAmount = account.InitialAmount;
                    accountToUpdate.Name = account.Name;
                }
            }
            context.SaveChanges();
        }
    }
}
