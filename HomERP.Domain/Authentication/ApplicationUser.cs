using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using HomERP.Domain.Entity;

namespace HomERP.Domain.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public Family Family { get; set; }
    }
}
