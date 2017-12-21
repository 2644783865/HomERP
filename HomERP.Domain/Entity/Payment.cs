using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using HomERP.Domain.Helpers;

namespace HomERP.Domain.Entity
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Kwota")]
        public decimal Amount { get; set; }
        public DateTime Time { get; set; }

        [Display(Name = "Konto")]
//        public int CashAccountId { get; set; }
//        [ForeignKey(nameof(CashAccountId))]
        public virtual CashAccount CashAccount { get; set; }
        public virtual Contractor Contractor { get; set; }

        public Payment()
        {
            CashAccount = new CashAccount();
        }
    }
}
