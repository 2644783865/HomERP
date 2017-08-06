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
        public IEnumerable<CashAccount> CashAccountList { get; set; }
        public IEnumerable<FamilyUser> FamilyUserList { get; set; }

        public PaymentEditVM() { }
        public PaymentEditVM(IPaymentProvider provider)
        {
            CashAccountList = provider.CashAccounts;
            FamilyUserList = provider.FamilyUsers;
        }
    }
}
