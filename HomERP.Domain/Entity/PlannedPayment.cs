﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HomERP.Domain.Helpers;

namespace HomERP.Domain.Entity
{
    public class PlannedPayment:Payment
    {
        public PaymentStatus Status { get; set; }
    }
}
