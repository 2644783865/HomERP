using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomERP.Domain.Entity;
using HomERP.Domain.Repository.Abstract;

namespace HomERP.Domain.Repository.EntityFramework
{
    public class EfFamilyUserRepository : IFamilyUserRepository
    {
        private EfDbContext context;
        public EfFamilyUserRepository(EfDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<FamilyUser> FamilyUsers
        {
            get { return context.FamilyUsers; }
        }

        public FamilyUser DeleteFamilyUser(int userId)
        {
            FamilyUser userToDelete = context.FamilyUsers.Find(userId);
            if(userToDelete!=null)
            {
                context.FamilyUsers.Remove(userToDelete);
            }
            context.SaveChanges();
            return userToDelete;
        }

        public void SaveFamilyUser(FamilyUser user)
        {
            if (user.Id==0)
            {
                context.FamilyUsers.Add(user);
            }
            else
            {
                FamilyUser userToUpdate = context.FamilyUsers.Find(user.Id);
                userToUpdate.Email = user.Email;
                userToUpdate.Name = user.Name;
                userToUpdate.PasswordHash = user.PasswordHash;
            }
            context.SaveChanges();
        }
    }
}
