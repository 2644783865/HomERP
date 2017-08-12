using System;
using System.Collections.Generic;
using System.Text;
using HomERP.Domain.Entity;
using HomERP.Domain.Authentication;

namespace HomERP.Domain.Logic.Abstract
{
    public interface IFamilyProvider
    {
        IEnumerable<Family> Families { get; }
        void SaveFamily(Family family);
        Family DeleteFamily(int familyId);
        Family FamilyForUser(ApplicationUser user);
    }
}
