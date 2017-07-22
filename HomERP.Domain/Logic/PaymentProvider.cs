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

        public IEnumerable<Payment> Payments
        { get { return paymentRepository.Payments; } }

        public Payment DeletePayment(int paymentId)
        {
            return paymentRepository.DeletePayment(paymentId);
        }

        public void SavePayment(Payment payment)
        {
            paymentRepository.SavePayment(payment);
        }
    }
}
