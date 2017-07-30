using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomERP.Domain.Entity;
using HomERP.Domain.Logic.Abstract;
using HomERP.Domain.Repository.EntityFramework;

namespace HomERP.WebUI.Controllers
{
    public class UserController : Controller
    {
        IUserProvider provider;
        public UserController(IUserProvider provider)
        {
            this.provider = provider;
        }
        public IActionResult Index()
        {
            return View(provider.Users);
        }

        public IActionResult Edit(int id)
        {
            User user = provider.Users.FirstOrDefault(u => u.Id == id);
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User model)
        {
            provider.SaveUser(model);
            return View("Index", provider.Users);
        }

        public IActionResult Add()
        {
            User user = new User();
            return View("Edit", user);
        }

        public IActionResult Delete(int id)
        {
            provider.DeleteUser(id);
            return View("Index", provider.Users);
        }
    }
}