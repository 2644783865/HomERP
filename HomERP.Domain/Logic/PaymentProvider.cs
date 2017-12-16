using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using HomERP.Domain.Entity;
using HomERP.Domain.Repository.Abstract;
using HomERP.Domain.Logic.Abstract;

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

        public bool DeletePayment(int paymentId)
        {
            return paymentRepository.DeletePayment(paymentId);
        }

        public bool SavePayment(Payment payment)
        {
            return paymentRepository.SavePayment(payment);
        }

        public IQueryable<CashAccount> CashAccounts
        { get { return paymentRepository.CashAccounts; } }
    }
}
