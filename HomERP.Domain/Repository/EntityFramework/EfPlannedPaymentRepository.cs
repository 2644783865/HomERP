using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomERP.Domain.Entity;
using HomERP.Domain.Repository.Abstract;

namespace HomERP.Domain.Repository.EntityFramework
{
    public class EfPlannedPaymentRepository : IPlannedPaymentRepository
    {
        private EfDbContext context;
        public EfPlannedPaymentRepository(EfDbContext context)
        {
            this.context = context;
        }

        public IQueryable<PlannedPayment> PlannedPayments
        {
            get { return context.PlannedPayments; }
        }

        public PlannedPayment DeletePlannedPayment(int paymentId)
        {
            PlannedPayment paymentToDelete = context.PlannedPayments.Find(paymentId);
            if(paymentToDelete!=null)
            {
                context.PlannedPayments.Remove(paymentToDelete);
            }
            context.SaveChanges();
            return paymentToDelete;
        }

        public void SavePlannedPayment(PlannedPayment payment)
        {
            if(payment.Id==0)
            {
                context.PlannedPayments.Add(payment);
            }
            else
            {
                PlannedPayment paymentToUpdate = context.PlannedPayments.Find(payment.Id);
                paymentToUpdate.CashAccount = payment.CashAccount;
                paymentToUpdate.Amount = payment.Amount;
                paymentToUpdate.Direction = payment.Direction;
                paymentToUpdate.Time = payment.Time;
                paymentToUpdate.Status = Helpers.PaymentStatus.Accepted;
            }
            context.SaveChanges();
        }
    }
}
