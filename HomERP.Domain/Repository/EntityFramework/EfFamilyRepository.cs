using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using HomERP.Domain.Authentication;
using HomERP.Domain.Entity;
using HomERP.Domain.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace HomERP.Domain.Repository.EntityFramework
{
    public class EfFamilyRepository : IFamilyRepository
    {
        private EfDbContext context;
        public EfFamilyRepository(EfDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Family> Families
        { get { return context.Families; } }

        public Family DeleteFamily(int familyId)
        {
            Family familyToDelete = context.Families.Find(familyId);
            if(familyToDelete!=null)
            {
                context.Families.Remove(familyToDelete);
                context.SaveChanges();
            }
            return familyToDelete;
        }

        public void SaveFamily(Family family)
        {
            if (family.Id==0)
            {
                context.Families.Add(family);
            }
            else
            {
                Family familyToUpdate = context.Families.Find(family.Id);
                familyToUpdate.Name = family.Name;
                familyToUpdate.Description = family.Description;
            }
            context.SaveChanges();
        }

        public Family FamilyForUser(ApplicationUser user)
        {
            return context.Families.FirstOrDefault(f=>f.Id == user.FamilyId);
        }
    }
}
