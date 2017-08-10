using System;
using System.Collections.Generic;
using System.Text;
using HomERP.Domain.Entity;
using HomERP.Domain.Repository.Abstract;
using HomERP.Domain.Logic.Abstract;

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

        public void SaveFamily(Family family)
        {
            repository.SaveFamily(family);
        }
    }
}
