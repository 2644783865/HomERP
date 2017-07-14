using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomERP.Domain.Entity.Abstract;
using HomERP.Domain.Repository.Abstract;

namespace HomERP.Domain.Repository.EntityFramework
{
    class EFAccountRepository : IAccountRepository
    {
        public IEnumerable<IAccount> Accounts
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IAccount DeleteAccount(int accountId)
        {
            throw new NotImplementedException();
        }

        public void SaveAccount(IAccount account)
        {
            throw new NotImplementedException();
        }
    }
}
