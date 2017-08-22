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
    }
}
