using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HomERP.Domain.Helpers;

namespace HomERP.Domain.Entity.Abstract
{
    interface IOperation
    {
        DateTime Time { get; set; }
        decimal Amount { get; set; }
        IUser User { get; set; }
        IAccount Account { get; set; }
        CashFlowDirection Direction { get; set; }

    }
}
