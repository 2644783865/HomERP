using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HomERP.Domain.Entity;

namespace HomERP.Domain.Repository.Abstract
{
    public interface IUserRepository
    {
        IEnumerable<User> Users { get; }
        void SaveUser(User user);
        User DeleteUser(int userId);
    }
}
