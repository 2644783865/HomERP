using System;
using System.Collections.Generic;
using System.Text;
using HomERP.Domain.Entity;

namespace HomERP.Domain.Repository.Abstract
{
    public interface IFamilyRepository
    {
        IEnumerable<Family> Families { get; }
        void SaveFamily(Family family);
        Family DeleteFamily(int familyId);
    }
}
