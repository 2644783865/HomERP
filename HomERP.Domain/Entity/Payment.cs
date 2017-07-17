using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HomERP.Domain.Entity.Abstract;
using HomERP.Domain.Helpers;

namespace HomERP.Domain.Entity
{
    public class Payment : IOperation
    {
        public int Id { get; set; }
        public IAccount Account { get; set; }
        public decimal Amount { get; set; }
        public CashFlowDirection Direction { get; set; }
        public DateTime Time { get; set; }
        public IUser User { get; set; }
    }
}
