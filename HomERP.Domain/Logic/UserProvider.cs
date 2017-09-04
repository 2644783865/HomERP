using HomERP.Domain.Logic.Abstract;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using HomERP.Domain.Authentication;
using HomERP.Domain.Repository.Abstract;
using HomERP.Domain.Entity;

namespace HomERP.Domain.Logic
{
    public class UserProvider : IUserProvider
    {
        IUserRepository repository;

        public UserProvider(IUserRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<ApplicationUser> GetFamilyMembers(Family family)
        {
            return repository.Users.Where(u=>u.Family.Id == family.Id);
        }

        public void SaveUser(ApplicationUser user)
        {
            this.repository.SaveUser(user);
        }
    }
}
