using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HomERP.WebUI.Models.Shared;
using HomERP.WebUI.Helpers;
using HomERP.WebUI.Handlers.Abstract;
using HomERP.WebUI.Models.CashAccount;

namespace HomERP.WebUI.Controllers
{
    [Authorize(Roles = "FamilyMember")]
    public class CashAccountController : Controller
    {
        private ICashAccountHandler handler;

        public CashAccountController(ICashAccountHandler handler)
        {
            this.handler = handler;
        }

        public IActionResult Index(int page)
        {
            PageInfo pageInfo = HttpContext.Session.Get<PageInfo>("cashaccountpageinfo");
            if (pageInfo == null)
            {
                pageInfo = new PageInfo();
            }
            if (page != 0)
            {
                pageInfo.CurrentPage = page;
            }
            var model = handler.GetList(pageInfo);
            HttpContext.Session.Set<PageInfo>("cashaccountpageinfo", pageInfo.ClearMessage());
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> GroupAction([FromQuery] int page, int[] id, string submit)
        {
            if (string.IsNullOrWhiteSpace(submit))
            {
                RedirectToAction("Index", page);
            }

            if (id == null)
            {
                RedirectToAction("Index", page);
            }

            if (submit.ToLower() == "delete")
            {
                PageInfo model = await handler.PerformDeletion(id, page);
                HttpContext.Session.Set<PageInfo>("cashaccountpageinfo", model);
                return RedirectToAction("Index", page);
            }

            return RedirectToAction("Index");
        }


        public IActionResult Edit(int id)
        {
            CashAccountVM account = handler.Edit(id);
            return View(account);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Edit(CashAccountVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            bool result = await handler.EditAsync(model);
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Zapis konta nie powiód³ siê!");
                return View(model);
            }
        }

        public IActionResult Add()
        {
            return View("Edit", new CashAccountVM());
        }
    }
}




