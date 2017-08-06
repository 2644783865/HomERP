using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HomERP.Domain.Entity;

namespace HomERP.Domain.Repository.Abstract
{
    public interface IFamilyUserRepository
    {
        IEnumerable<FamilyUser> FamilyUsers { get; }
        void SaveFamilyUser(FamilyUser user);
        FamilyUser DeleteFamilyUser(int userId);
    }
}
