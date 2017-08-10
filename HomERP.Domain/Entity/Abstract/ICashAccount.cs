﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomERP.Domain.Entity.Abstract
{
    public interface ICashAccount
    {
        int Id { get; set; }
        string Name { get; set; }
        decimal InitialAmount { get; set; }
    }
}