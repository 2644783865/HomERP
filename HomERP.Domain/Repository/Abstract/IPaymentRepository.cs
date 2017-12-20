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
        IQueryable<Payment> Payments { get; }
        Task<bool> SavePaymentAsync(Payment payment);
        Task<bool> DeletePaymentAsync(int paymentId);

        IQueryable<CashAccount> CashAccounts { get; }
    }
}
