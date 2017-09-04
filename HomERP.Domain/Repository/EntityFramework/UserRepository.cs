using HomERP.Domain.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using HomERP.Domain.Authentication;

namespace HomERP.Domain.Repository.EntityFramework
{
    public class UserRepository : IUserRepository
    {
        private EfDbContext context;
        public UserRepository(EfDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<ApplicationUser> Users
        {
            get
            {
                return context.Users;
            }
        }

        public void SaveUser(ApplicationUser user)
        {
            if (user.Id == string.Empty)
            {
                throw new Exception("Can't happen!");
            }
            else
            {
                ApplicationUser userToUpdate = context.Users.Find(user.Id);
                if (userToUpdate!=null)
                {
                    userToUpdate.FamilyId = user.FamilyId;
                }
            }
            context.SaveChanges();
        }
    }
}
