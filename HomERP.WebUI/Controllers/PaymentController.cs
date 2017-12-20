using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomERP.WebUI.Models.PaymentViewModels;
using Microsoft.AspNetCore.Authorization;
using HomERP.WebUI.Handlers.Abstract;

namespace HomERP.WebUI.Controllers
{
    [Authorize(Roles = "FamilyMember")]
    public class PaymentController : Controller
    {
        private IPaymentHandler handler;
        public PaymentController(IPaymentHandler handler)
        {
            this.handler = handler;
        }

        public IActionResult Index()
        {
            return View(handler.Payments);
        }

        public IActionResult Edit(int id)
        {
            PaymentEditVM model = handler.Edit(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PaymentEditVM model)
        {
            if (ModelState.IsValid)
            {
                await handler.SaveAsync(model);
                return RedirectToAction("Index");
            }
            else
            {
                model = handler.Edit(model);
                return View(model);
            }
        }

        public IActionResult Add()
        {
            PaymentEditVM model = handler.Edit();
            return View("Edit", model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await handler.DeleteAsync(id);
            return RedirectToAction("Index", handler.Payments);
        }
    }
}