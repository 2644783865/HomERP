using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomERP.Domain.Helpers;

namespace HomERP.Domain.Entity.Abstract
{
    interface ISubject
    {
        SubjectKind Kind { get; }
        string Name { get; set; }
    }
}
