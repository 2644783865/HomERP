using HomERP.WebUI.Models;
using HomERP.WebUI.Models.PaymentViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomERP.WebUI.Handlers.Abstract
{
    public interface IPaymentHandler
    {
        //lista płatności do wyświetlenia w /Index
         IEnumerable<PaymentVM> Payments {get;}
        // wybrana płatność do edycji
        PaymentEditVM Edit(int id = 0);
        PaymentEditVM Edit(PaymentEditVM model);
        Task<bool> SaveAsync(PaymentEditVM model);
        Task<bool> DeleteAsync(int id);
    }
}
