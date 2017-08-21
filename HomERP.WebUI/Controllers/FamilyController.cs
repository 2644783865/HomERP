using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomERP.Domain.Logic.Abstract;
using HomERP.Domain.Entity;
using HomERP.Domain.Authentication;
using HomERP.Domain.Authentication.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using HomERP.WebUI.Models.FamilyViewModels;

namespace HomERP.WebUI.Controllers
{
    [Authorize]
    public class FamilyController : Controller
    {
        IFamilyProvider provider;
        private readonly UserManager<ApplicationUser> userManager;

        public FamilyController(IFamilyProvider provider, UserManager<ApplicationUser> userManager)
        {
            this.provider = provider;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            ApplicationUser currentUser = await userManager.GetUserAsync(User);
            FamilyOverviewVM model = new FamilyOverviewVM
            {
                Family = provider.FamilyForUser(currentUser),
                FamilyMembers = new ApplicationUser[] { currentUser }
            };
            return View(model);
        }
    }
}