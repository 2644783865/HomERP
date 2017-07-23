using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomERP.Domain.Helpers
{
    public enum CashFlowDirection
    {
        Increase = 1,
        Decrease = 0
    }

    public static class CashFlowDirectionExtensions
    {
        public static string ToString(this CashFlowDirection direction)
        {
            if (direction == CashFlowDirection.Increase)
                return "Zwiększenie";
            else
                return "Zmniejszenie";
        }
    }
}
