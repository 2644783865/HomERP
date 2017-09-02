using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using HomERP.Domain.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomERP.Domain.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Overriden from base class in order to localize annotations.
        /// </summary>
        [Display(Name ="Nazwa użytkownika")]
        public override string UserName { get; set; }

        public int? FamilyId { get; set; }
        [ForeignKey(nameof(FamilyId))]
        public virtual Family Family { get; set; }
    }
}
