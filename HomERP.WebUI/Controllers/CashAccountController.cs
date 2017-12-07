using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomERP.Domain.Entity;
using HomERP.Domain.Logic.Abstract;
using Microsoft.AspNetCore.Authorization;

namespace HomERP.WebUI.Controllers
{
    [Authorize(Roles = "FamilyMember")]
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
            CashAccount cashAccount = provider.CashAccounts.FirstOrDefault(a => a.Id == id);
            return View(cashAccount);
        }

        [HttpPost]
        public IActionResult Edit(CashAccount cashAccount)
        {
            if(!ModelState.IsValid)
            {
                return View(cashAccount);
            }
            provider.SaveCashAccount(cashAccount);
            return View("Index", provider.CashAccounts);
        }

        public IActionResult Add()
        {
            CashAccount cashAccount = new CashAccount();
            return View("Edit", cashAccount);
        }

        public IActionResult Delete(int id)
        {
            provider.DeleteCashAccount(id);
            return View("Index", provider.CashAccounts);
        }
    }
}