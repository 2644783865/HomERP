using System;
using System.Collections.Generic;
using System.Text;

namespace HomERP.Domain.Authentication
{
    public interface IApplicationUser
    {
        int? FamilyId { get; set; }
    }
}
