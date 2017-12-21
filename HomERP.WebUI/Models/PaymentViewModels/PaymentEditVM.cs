using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomERP.Domain.Entity;
using HomERP.Domain.Logic.Abstract;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HomERP.WebUI.Models.PaymentViewModels
{
    public class PaymentEditVM
    {
        public int Id { get; set; }
        [Display(Name = "Kwota")]
        public decimal Amount { get; set; }
        public DateTime Time { get; set; }
        [Display(Name = "Konto")]
        public int CashAccountId { get; set; }

        public IEnumerable<SelectListItem> CashAccountList { get; set; }

        public PaymentEditVM() { }
        public PaymentEditVM(IPaymentProvider provider)
        {
            CashAccountList = provider.CashAccounts.Select(item => new SelectListItem()
            {
                 Value = item.Id.ToString(), Text = item.Name
            });
        }
    }
}
