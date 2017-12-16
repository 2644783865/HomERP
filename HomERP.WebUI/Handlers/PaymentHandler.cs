using HomERP.WebUI.Handlers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomERP.WebUI.Models.PaymentViewModels;
using HomERP.Domain.Logic.Abstract;
using Microsoft.AspNetCore.Mvc.Rendering;
using HomERP.WebUI.ModelMapping;

namespace HomERP.WebUI.Handlers
{
    public class PaymentHandler : IPaymentHandler
    {
        private IPaymentProvider provider;

        public PaymentHandler(IPaymentProvider provider)
        {
            this.provider = provider;
        }

        public IEnumerable<PaymentVM> Payments => provider.Payments.Select(p=>p.ToViewModel());

        public bool Delete(int id)
        {
            return provider.DeletePayment(id);
        }

        public PaymentEditVM Edit(int id)
        {
            PaymentEditVM model = new PaymentEditVM(provider);
            if (id==0)
            {
                return model;
            }
            return model.AddEntity(provider.Payments.FirstOrDefault(p => p.Id == id));
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
            provider.SavePayment(model.ToEntity());
            return true;
            //throw new NotImplementedException();
        }




    }
}
