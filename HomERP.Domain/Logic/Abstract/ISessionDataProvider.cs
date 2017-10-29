using HomERP.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomERP.Domain.Logic.Abstract
{
    public interface ISessionDataProvider
    {
        Family Family { get; set; }
    }
}
