using System;
using System.Collections.Generic;
using System.Text;
using HomERP.Domain.Entity;
using HomERP.Domain.Logic.Abstract;
using HomERP.Domain.Repository.Abstract;

namespace HomERP.Domain.Logic
{
    public class FamilyUserProvider : IFamilyUserProvider
    {
        private IFamilyUserRepository repository;

        public FamilyUserProvider(IFamilyUserRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<FamilyUser> FamilyUsers
        { get { return repository.FamilyUsers; } }

        public FamilyUser DeleteFamilyUser(int userId)
        {
            return repository.DeleteFamilyUser(userId);
        }

        public void SaveFamilyUser(FamilyUser user)
        {
            repository.SaveFamilyUser(user);
        }
    }
}
