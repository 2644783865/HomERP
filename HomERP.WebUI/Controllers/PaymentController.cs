using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomERP.Domain.Logic.Abstract;
using HomERP.Domain.Entity;
using HomERP.WebUI.Models;
using HomERP.Domain.Repository.EntityFramework;

namespace HomERP.WebUI.Controllers
{
    public class PaymentController : Controller
    {
        private IPaymentProvider provider;
        public PaymentController(IPaymentProvider provider)
        {
            this.provider = provider;
        }

        public IActionResult Index()
        {
            return View(provider.Payments);
        }

        public IActionResult Edit(int id)
        {
            Payment payment = provider.Payments.FirstOrDefault(p => p.Id == id);
            PaymentEditVM model = new PaymentEditVM(provider)
            {
                Payment = payment,
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(PaymentEditVM model1)
        {
            if (ModelState.IsValid)
            {
                provider.SavePayment(model1.Payment);
                return RedirectToAction("Index");
            }
            else
            {
                model1.AccountList = provider.Accounts;
                model1.UserList = provider.Users;
                return View(model1);
            }
        }

        public IActionResult Add()
        {
            PaymentEditVM model = new PaymentEditVM(provider)
            {
                Payment = new Payment() { Time = DateTime.Now.Date }
            };
            return View("Edit", model);
        }

        public IActionResult Delete(int id)
        {
            provider.DeletePayment(id);
            return RedirectToAction("Index", provider.Payments);
        }
    }
}