using HomERP.WebUI.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomERP.WebUI.Models.CashAccount
{
    public class CashAccountListVM
    {
        public IEnumerable<CashAccountVM> CashAccounts { get; set; }
        public PageInfo PageInfo { get; set; }

        public CashAccountListVM()
        {
            PageInfo = new PageInfo();
        }
    }
}
