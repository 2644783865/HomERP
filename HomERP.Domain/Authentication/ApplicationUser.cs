using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using HomERP.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace HomERP.Domain.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Overriden from base class in order to localize annotations.
        /// </summary>
        [Display(Name ="Nazwa użytkownika")]
        public override string UserName { get; set; }

        public Family Family { get; set; }
    }
}
