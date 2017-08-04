using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomERP.Domain.Entity;
using HomERP.Domain.Logic.Abstract;

namespace HomERP.WebUI.Controllers
{
    public class CashAccountController : Controller
    {
        private IAccountProvider provider;

        public CashAccountController(IAccountProvider provider)
        {
            this.provider = provider;
        }

        public IActionResult Index()
        {
            return View(provider.Accounts);
        }

        public IActionResult Edit(int id)
        {
            Account cashAccount = provider.Accounts.FirstOrDefault(a => a.Id == id);
            return View(cashAccount);
        }

        [HttpPost]
        public IActionResult Edit(Account cashAccount)
        {
            provider.SaveAccount(cashAccount);
            return View("Index", provider.Accounts);
        }

        public IActionResult Add()
        {
            Account cashAaccount = new Account();
            return View("Edit", cashAaccount);
        }

        public IActionResult Delete(int id)
        {
            provider.DeleteAccount(id);
            return View("Index", provider.Accounts);
        }
    }
}