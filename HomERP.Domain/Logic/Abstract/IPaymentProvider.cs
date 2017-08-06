using HomERP.Domain.Entity;
using HomERP.Domain.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomERP.Domain.Logic.Abstract
{
    public interface IPaymentProvider
    {
        IEnumerable<Payment> Payments { get; }
        void SavePayment(Payment payment);
        Payment DeletePayment(int paymentId);

        IEnumerable<CashAccount> CashAccounts { get; }
        IEnumerable<FamilyUser> FamilyUsers { get; }
    }
}
