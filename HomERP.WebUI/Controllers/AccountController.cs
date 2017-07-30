using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomERP.Domain.Entity;
using HomERP.Domain.Logic.Abstract;

namespace HomERP.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IAccountProvider provider;

        public AccountController(IAccountProvider provider)
        {
            this.provider = provider;
        }

        public IActionResult Index()
        {
            return View(provider.Accounts);
        }

        public IActionResult Edit(int id)
        {
            Account account = provider.Accounts.FirstOrDefault(a => a.Id == id);
            return View(account);
        }

        [HttpPost]
        public IActionResult Edit(Account account)
        {
            provider.SaveAccount(account);
            return View("Index", provider.Accounts);
        }

        public IActionResult Add()
        {
            Account account = new Account();
            return View("Edit", account);
        }

        public IActionResult Delete(int id)
        {
            provider.DeleteAccount(id);
            return View("Index", provider.Accounts);
        }
    }
}