using System;
using System.Collections.Generic;
using System.Text;
using HomERP.Domain.Entity;

namespace HomERP.Domain.Logic.Abstract
{
    public interface IUserProvider
    {
        IEnumerable<User> Users { get; }
        void SaveUser(User user);
        User DeleteUser(int userId);
    }
}
