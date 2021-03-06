﻿using HomERP.Domain.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomERP.Domain.Repository.Abstract
{
    public interface IUserRepository
    {
        IQueryable<ApplicationUser> Users { get; }
        void SaveUser(ApplicationUser user);
    }
}
