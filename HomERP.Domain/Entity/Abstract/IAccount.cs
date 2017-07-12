﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomERP.Domain.Entity.Abstract
{
    interface IAccount
    {
        string Name { get; set; }
        IUser[] Owner { get; set; }
        decimal InitialAmount { get; set; }
    }
}
