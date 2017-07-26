using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomERP.Domain.Logic.Abstract;

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
    }
}