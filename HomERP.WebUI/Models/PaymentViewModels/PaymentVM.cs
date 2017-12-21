using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomERP.WebUI.Models.PaymentViewModels
{
    public class PaymentVM
    {
        public int Id { get; set; }
        [Display(Name = "Kwota")]
        public decimal Amount { get; set; }
        [Display(Name = "Data")]
        public DateTime Time { get; set; }
        [Display(Name = "Konto")]
        public string CashAccountName { get; set; }
    }
}
