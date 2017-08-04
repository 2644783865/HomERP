﻿using System;
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

        public IEnumerable<Payment> Payments
        {
            get { return context.Payments.Include(p=>p.CashAccount).Include(p=>p.User); }
        }

        public Payment DeletePayment(int paymentId)
        {
            Payment paymentToDelete = context.Payments.Find(paymentId);
            if(paymentToDelete!=null)
            {
                context.Payments.Remove(paymentToDelete);
            }
            context.SaveChanges();
            return paymentToDelete;
        }

        public void SavePayment(Payment payment)
        {
            payment.CashAccount = this.Accounts.First(a=>a.Id == payment.CashAccount.Id);
            payment.User = this.Users.First(u => u.Id == payment.User.Id);
            if(payment.Id==0)
            {
                context.Payments.Add(payment);
            }
            else
            {
                Payment paymentToUpdate = context.Payments.Find(payment.Id);
                paymentToUpdate.CashAccount = payment.CashAccount;
                paymentToUpdate.Amount = payment.Amount;
                paymentToUpdate.Direction = payment.Direction;
                paymentToUpdate.Time = payment.Time;
                paymentToUpdate.User = payment.User;
            }
            context.SaveChanges();
        }

        public IEnumerable<CashAccount> Accounts
        {
            get { return context.CashAccounts; }
        }

        public IEnumerable<User> Users
        {
            get { return context.Users; }
        }
    }
}
