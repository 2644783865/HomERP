using HomERP.Domain.Entity;
using HomERP.Domain.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomERP.Domain.Logic.Abstract
{
    public interface IPaymentProvider
    {
        IQueryable<Payment> Payments { get; }
        void SavePayment(Payment payment);
        Payment DeletePayment(int paymentId);

        IQueryable<CashAccount> CashAccounts { get; }
    }
}
