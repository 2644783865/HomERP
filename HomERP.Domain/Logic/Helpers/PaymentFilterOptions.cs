using HomERP.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomERP.Domain.Logic.Helpers
{
    public class PaymentFilterOptions
    {
        public Func<Payment, bool> a = new Func<Payment, bool>(a => a.Amount < 100);
    }
}
