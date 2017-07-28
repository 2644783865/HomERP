using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomERP.Domain.Entity;

namespace HomERP.WebUI.Models
{
    public class PaymentEditVM
    {
        public Payment Payment { get; set; }
        public IEnumerable<Account> AccountList { get; set; }
        public IEnumerable<User> UserList { get; set; }
    }
}
