﻿using System;
using System.Collections.Generic;
using System.Text;
using HomERP.Domain.Entity;
using HomERP.Domain.Authentication;
using System.Linq;

namespace HomERP.Domain.Repository.Abstract
{
    public interface IFamilyRepository
    {
        IQueryable<Family> Families { get; }
        void SaveFamily(Family family);
        Family DeleteFamily(int familyId);
        Family FamilyForUser(ApplicationUser user);
    }
}
