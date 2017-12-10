using HomERP.WebUI.Handlers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomERP.WebUI.Models.PaymentViewModels;
using HomERP.Domain.Logic.Abstract;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HomERP.WebUI.Handlers
{
    public class PaymentHandler : IPaymentHandler
    {
        private IPaymentProvider provider;

        public PaymentHandler(IPaymentProvider provider)
        {
            this.provider = provider;
        }

        public IEnumerable<PaymentVM> Payments => provider.Payments.Select(p=>EntityToView(p));

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public PaymentEditVM Edit(int id)
        {
            PaymentEditVM model = new PaymentEditVM(provider);
            if (id>0)
            {
                var payment = provider.Payments.FirstOrDefault(p=>p.Id == id);
                model.Id = payment.Id;
                model.Amount = payment.Amount;
                model.CashAccountId = payment.CashAccount.Id;
                model.Time = payment.Time;
            }
            return model;
            //throw new NotImplementedException();
        }

        public PaymentEditVM Edit(PaymentEditVM model)
        {
            model.CashAccountList = provider.CashAccounts.Select(item => new SelectListItem()
            {
                Value = item.Id.ToString(),
                Text = item.Name
            });
            return model;
            //throw new NotImplementedException();
        }

        public bool Save(PaymentEditVM model)
        {
            provider.SavePayment(ViewToEntity(model));
            return true;
            //throw new NotImplementedException();
        }

        private PaymentVM EntityToView(Domain.Entity.Payment payment)
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

        private Domain.Entity.Payment ViewToEntity(PaymentEditVM paymentVM)
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


    }
}
