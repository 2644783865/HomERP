using HomERP.WebUI.Models;
using HomERP.WebUI.Models.CashAccount;
using HomERP.WebUI.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomERP.WebUI.Handlers.Abstract
{
    public interface ICashAccountHandler
    {
        //lista płatności do wyświetlenia w /Index
        CashAccountListVM GetList(PageInfo pageInfo);
        // wybrana płatność do edycji
        CashAccountVM Edit(int id = 0);
        Task<bool> EditAsync(CashAccountVM model);
        Task<bool> DeleteRangeAsync(IEnumerable<int> identifiers);
        Task<PageInfo> PerformDeletion(int[] id, int page);
    }
}
