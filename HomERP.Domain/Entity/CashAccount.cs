using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomERP.Domain.Entity.Abstract;
using System.ComponentModel.DataAnnotations;

namespace HomERP.Domain.Entity
{
    public class CashAccount : ICashAccount
    {
        public int Id { get; set; }
        [Display(Name = "Stan początkowy")]
        public decimal InitialAmount { get; set; }
        [Display(Name="Nazwa konta")]
        public string Name { get; set; }
    }
}
