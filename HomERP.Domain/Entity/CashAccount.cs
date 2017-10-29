using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HomERP.Domain.Entity
{
    public class CashAccount
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Stan początkowy")]
        public decimal InitialAmount { get; set; }
        [Display(Name="Nazwa konta")]
        public string Name { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }

        public int FamilyId { get; set; }
        public Family Family { get; set; }
    }
}
