using System;
using System.Collections.Generic;
using System.Text;
using HomERP.Domain.Entity;
using HomERP.Domain.Authentication;
using System.Linq;

namespace HomERP.Domain.Logic.Abstract
{
    public interface IFamilyProvider
    {
        IQueryable<Family> Families { get; }
        bool SaveFamily(Family family, IApplicationUser currentUser);
        Family DeleteFamily(int familyId);
        Family FamilyForUser(ApplicationUser user);
    }
}
