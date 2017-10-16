using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HomERP.Domain.Entity;

namespace HomERP.Domain.Repository.Abstract
{
    public interface IPaymentRepository
    {
        IEnumerable<Payment> Payments { get; }
        void SavePayment(Payment payment);
        Payment DeletePayment(int paymentId);

        IEnumerable<CashAccount> CashAccounts { get; }
    }
}
