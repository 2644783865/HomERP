using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomERP.Domain.Helpers;

namespace HomERP.Domain.Entity
{
    public class Contractor
    {
        public ContractorKind Kind { get; } = ContractorKind.Company;

        public string Name { get; set; }
    }
}
