using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomERP.Domain.Entity;
using HomERP.Domain.Logic.Abstract;

namespace HomERP.WebUI.Models
{
    public class PaymentEditVM
    {
        public Payment Payment { get; set; }
        public IEnumerable<Account> CashAccountList { get; set; }
        public IEnumerable<User> UserList { get; set; }

        public PaymentEditVM() { }
        public PaymentEditVM(IPaymentProvider provider)
        {
            CashAccountList = provider.CashAccounts;
            UserList = provider.Users;
        }
    }
}
