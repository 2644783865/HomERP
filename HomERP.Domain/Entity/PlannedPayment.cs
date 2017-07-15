using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HomERP.Domain.Entity.Abstract;
using HomERP.Domain.Helpers;

namespace HomERP.Domain.Entity
{
    public class PlannedPayment:Payment, IOperation
    {
        PaymentStatus Status { get; set; }
    }
}
