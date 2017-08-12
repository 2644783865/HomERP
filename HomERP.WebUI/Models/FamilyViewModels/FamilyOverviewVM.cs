using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomERP.Domain.Entity;
using HomERP.Domain.Authentication;

namespace HomERP.WebUI.Models.FamilyViewModels
{
    public class FamilyOverviewVM
    {
        public Family Family { get; set; }
        public IEnumerable<ApplicationUser> FamilyMembers { get; set; }
    }
}
