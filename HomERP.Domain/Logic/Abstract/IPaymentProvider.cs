using HomERP.Domain.Entity;
using HomERP.Domain.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomERP.Domain.Logic.Abstract
{
    public interface IPaymentProvider
    {
        IQueryable<Payment> Payments { get; }
        Task<bool> SavePaymentAsync(Payment payment);
        Task<bool> DeletePaymentAsync(int paymentId);

        IQueryable<CashAccount> CashAccounts { get; }
    }
}
