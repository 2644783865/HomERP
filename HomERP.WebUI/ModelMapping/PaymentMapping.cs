using HomERP.WebUI.Models.PaymentViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomERP.WebUI.ModelMapping
{
    public static class PaymentMapping
    {
        public static PaymentVM ToViewModel(this Domain.Entity.Payment payment)
        {
            PaymentVM viewModel = new PaymentVM
            {
                Id = payment.Id,
                Amount = payment.Amount,
                CashAccountName = payment.CashAccount.Name,
                Time = payment.Time
            };
            return viewModel;
        }

        public static Domain.Entity.Payment ToEntity(this PaymentEditVM paymentVM)
        {
            Domain.Entity.Payment payment = new Domain.Entity.Payment
            {
                Id = paymentVM.Id,
                Amount = paymentVM.Amount,
                CashAccount = new Domain.Entity.CashAccount { Id = paymentVM.CashAccountId },
                Time = paymentVM.Time
            };
            return payment;
        }

        public static PaymentEditVM AddEntity(this PaymentEditVM paymentVM, Domain.Entity.Payment payment)
        {
            paymentVM.Id = payment.Id;
            paymentVM.Amount = payment.Amount;
            paymentVM.CashAccountId = payment.CashAccount.Id;
            paymentVM.Time = payment.Time;
            return paymentVM;
        }
    }
}
