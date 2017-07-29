using System;
using System.Collections.Generic;
using System.Text;
using HomERP.Domain.Entity;
using HomERP.Domain.Logic.Abstract;
using HomERP.Domain.Repository.Abstract;

namespace HomERP.Domain.Logic
{
    public class UserProvider : IUserProvider
    {
        private IUserRepository repository;

        public UserProvider(IUserRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<User> Users
        { get { return repository.Users; } }

        public User DeleteUser(int userId)
        {
            return repository.DeleteUser(userId);
        }

        public void SaveUser(User user)
        {
            repository.SaveUser(user);
        }
    }
}
