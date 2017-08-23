using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

using HomERP.Domain.Helpers;

namespace HomERP.Domain.Entity
{
    public class Payment
    {
        public int Id { get; set; }
        [Display(Name = "Konto")]
        public CashAccount CashAccount { get; set; }
        [Display(Name = "Kwota")]
        public decimal Amount { get; set; }
        public CashFlowDirection Direction { get; set; }
        public DateTime Time { get; set; }
        public FamilyUser FamilyUser { get; set; }

        public Payment()
        {
            CashAccount = new CashAccount();
            FamilyUser = new FamilyUser();
        }
    }
}
