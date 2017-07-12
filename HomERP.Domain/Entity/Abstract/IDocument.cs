using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HomERP.Domain.Entity;

namespace HomERP.Domain.Entity.Abstract
{
    interface IDocument
    {
        PlannedPayment Payment { get; set; }
    }
}
