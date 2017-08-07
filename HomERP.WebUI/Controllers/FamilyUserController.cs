using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomERP.Domain.Entity;
using HomERP.Domain.Logic.Abstract;
using HomERP.Domain.Repository.EntityFramework;
using Microsoft.AspNetCore.Authorization;

namespace HomERP.WebUI.Controllers
{
    [Authorize]
    public class FamilyUserController : Controller
    {
        IFamilyUserProvider provider;
        public FamilyUserController(IFamilyUserProvider provider)
        {
            this.provider = provider;
        }
        public IActionResult Index()
        {
            return View(provider.FamilyUsers);
        }

        public IActionResult Edit(int id)
        {
            FamilyUser user = provider.FamilyUsers.FirstOrDefault(u => u.Id == id);
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(FamilyUser model)
        {
            provider.SaveFamilyUser(model);
            return View("Index", provider.FamilyUsers);
        }

        public IActionResult Add()
        {
            FamilyUser user = new FamilyUser();
            return View("Edit", user);
        }

        public IActionResult Delete(int id)
        {
            provider.DeleteFamilyUser(id);
            return View("Index", provider.FamilyUsers);
        }
    }
}