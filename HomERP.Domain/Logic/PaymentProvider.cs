using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using HomERP.Domain.Entity;
using HomERP.Domain.Repository.Abstract;
using HomERP.Domain.Logic.Abstract;
using System.Threading.Tasks;

namespace HomERP.Domain.Logic
{
    public class PaymentProvider: IPaymentProvider
    {
        IPaymentRepository paymentRepository;

        public PaymentProvider(IPaymentRepository repository)
        {
            this.paymentRepository = repository;
        }

        public IQueryable<Payment> Payments
        { get { return paymentRepository.Payments; } }

        public async Task<bool> DeletePaymentAsync(int paymentId)
        {
            return await paymentRepository.DeletePaymentAsync(paymentId);
        }

        public async Task<bool> SavePaymentAsync(Payment payment)
        {
            return await paymentRepository.SavePaymentAsync(payment);
        }

        public IQueryable<CashAccount> CashAccounts
        { get { return paymentRepository.CashAccounts; } }
    }
}
