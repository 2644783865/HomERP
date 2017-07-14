using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HomERP.Domain.Entity.Abstract;

namespace HomERP.Domain.Repository.Abstract
{
    public interface IAccountRepository
    {
        IEnumerable<IAccount> Accounts { get; }
        void SaveAccount(IAccount account);
        IAccount DeleteAccount(int accountId);
    }
}
