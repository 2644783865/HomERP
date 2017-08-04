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
        private ICashAccountProvider provider;

        public CashAccountController(ICashAccountProvider provider)
        {
            this.provider = provider;
        }

        public IActionResult Index()
        {
            return View(provider.CashAccounts);
        }

        public IActionResult Edit(int id)
        {
            Account cashAccount = provider.CashAccounts.FirstOrDefault(a => a.Id == id);
            return View(cashAccount);
        }

        [HttpPost]
        public IActionResult Edit(Account cashAccount)
        {
            provider.SaveCashAccount(cashAccount);
            return View("Index", provider.CashAccounts);
        }

        public IActionResult Add()
        {
            Account cashAaccount = new Account();
            return View("Edit", cashAaccount);
        }

        public IActionResult Delete(int id)
        {
            provider.DeleteCashAccount(id);
            return View("Index", provider.CashAccounts);
        }
    }
}