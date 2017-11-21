using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HomERP.Domain.Entity;

namespace HomERP.Domain.Repository.Abstract
{
    public interface IPlannedPaymentRepository
    {
        IQueryable<PlannedPayment> PlannedPayments { get; }
        void SavePlannedPayment(PlannedPayment payment);
        PlannedPayment DeletePlannedPayment(int paymentId);
    }
}
