using System;
using System.Collections.Generic;
using System.Text;
using HomERP.Domain.Entity;

namespace HomERP.Domain.Logic.Abstract
{
    public interface IFamilyUserProvider
    {
        IEnumerable<FamilyUser> FamilyUsers { get; }
        void SaveFamilyUser(FamilyUser user);
        FamilyUser DeleteFamilyUser(int userId);
    }
}
