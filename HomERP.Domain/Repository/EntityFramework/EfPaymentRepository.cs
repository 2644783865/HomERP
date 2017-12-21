using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomERP.Domain.Entity;
using HomERP.Domain.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace HomERP.Domain.Repository.EntityFramework
{
    public class EfPaymentRepository : IPaymentRepository
    {
        private EfDbContext context;
        public EfPaymentRepository(EfDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Payment> Payments
        {
            get { return context.Payments.Include(p=>p.CashAccount); }
        }

        public async Task<bool> DeletePaymentAsync(int paymentId)
        {
            Payment paymentToDelete = context.Payments.Find(paymentId);
            if(paymentToDelete!=null)
            {
                context.Payments.Remove(paymentToDelete);
            }
            int result = await context.SaveChangesAsync();
            return result == 1;
        }

        public async Task<bool> SavePaymentAsync(Payment payment)
        {
            payment.CashAccount = this.CashAccounts.First(a=>a.Id == payment.CashAccount.Id);
            if(payment.Id==0)
            {
                context.Payments.Add(payment);
            }
            else
            {
                Payment paymentToUpdate = context.Payments.Find(payment.Id);
                paymentToUpdate.CashAccount = payment.CashAccount;
                paymentToUpdate.Amount = payment.Amount;
                paymentToUpdate.Time = payment.Time;
                if (context.Entry(paymentToUpdate).State == EntityState.Unchanged)
                {
                    return true;
                }
            }
            int result = await context.SaveChangesAsync();
            return result == 1;
        }

        public IQueryable<CashAccount> CashAccounts
        {
            get { return context.CashAccounts; }
        }
    }
}
