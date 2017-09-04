using HomERP.Domain.Authentication;
using HomERP.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomERP.Domain.Logic.Abstract
{
    public interface IUserProvider
    {
        IEnumerable<ApplicationUser> GetFamilyMembers(Family family);
        void SaveUser(ApplicationUser user);
    }
}
