using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomERP.Domain.Helpers;

namespace HomERP.Domain.Entity
{
    public class Company
    {
        public SubjectKind Kind { get; } = SubjectKind.Company;

        public string Name { get; set; }
    }
}
