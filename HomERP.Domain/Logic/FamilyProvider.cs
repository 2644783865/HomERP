using System;
using System.Collections.Generic;
using System.Text;
using HomERP.Domain.Entity;
using HomERP.Domain.Repository.Abstract;
using HomERP.Domain.Logic.Abstract;
using HomERP.Domain.Authentication;

namespace HomERP.Domain.Logic
{
    public class FamilyProvider : IFamilyProvider
    {
        private IFamilyRepository repository;
        public FamilyProvider(IFamilyRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Family> Families { get { return repository.Families; } }

        public Family DeleteFamily(int familyId)
        {
            return repository.DeleteFamily(familyId);
        }

        public bool SaveFamily(Family family, ApplicationUser currentUser)
        {
            if (currentUser.FamilyId == null && family.Id == 0
                || currentUser.FamilyId == family.Id)
            {
                repository.SaveFamily(family);
                return true;
            }
            return false;
        }

        public Family FamilyForUser(ApplicationUser user)
        {
            return repository.FamilyForUser(user);
        }
    }
}
