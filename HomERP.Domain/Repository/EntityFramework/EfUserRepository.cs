using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomERP.Domain.Entity;
using HomERP.Domain.Repository.Abstract;

namespace HomERP.Domain.Repository.EntityFramework
{
    public class EfUserRepository : IUserRepository
    {
        private EfDbContext context;
        public EfUserRepository(EfDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<User> Users
        {
            get { return context.Users; }
        }

        public User DeleteUser(int userId)
        {
            User userToDelete = context.Users.Find(userId);
            if(userToDelete!=null)
            {
                context.Users.Remove(userToDelete);
            }
            return userToDelete;
        }

        public void SaveUser(User user)
        {
            if (user.Id==0)
            {
                context.Users.Add(user);
            }
            else
            {
                User userToUpdate = context.Users.Find(user.Id);
                userToUpdate.Email = user.Email;
                userToUpdate.Name = user.Name;
                userToUpdate.PasswordHash = user.PasswordHash;
            }
            context.SaveChanges();
        }
    }
}
