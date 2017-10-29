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
        IUserProvider userProvider;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ISessionDataProvider sessionDataProvider;

        public FamilyController(IFamilyProvider provider,
            IUserProvider userProvider, 
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ISessionDataProvider sessionDataProvider)
        {
            this.provider = provider;
            this.userProvider = userProvider;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.sessionDataProvider = sessionDataProvider;
        }

        public async Task<IActionResult> Index()
        {
            ApplicationUser currentUser = await userManager.GetUserAsync(User);
            FamilyOverviewVM model = new FamilyOverviewVM
            {
                Family = provider.FamilyForUser(currentUser)                
            };
            model.FamilyMembers = userProvider.GetFamilyMembers(model.Family);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            ApplicationUser currentUser = await userManager.GetUserAsync(User);
            Family model = provider.FamilyForUser(currentUser);
            if (model == null) model = new Family();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Family model)
        {
            ApplicationUser currentUser = await userManager.GetUserAsync(User);
            if (provider.SaveFamily(model, currentUser))
            {
                if (currentUser.FamilyId == null)
                {
                    currentUser.FamilyId = model.Id;
                    userProvider.SaveUser(currentUser);
                    sessionDataProvider.Family = model;
                }
                await userManager.AddToRoleAsync(currentUser, "FamilyMember");
                await signInManager.SignInAsync(currentUser, isPersistent: false);
                return RedirectToAction("Index");
            }
            else
                return View(model);
        }

    }
}