using HomERP.WebUI.Handlers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomERP.Domain.Logic.Abstract;
using Microsoft.AspNetCore.Mvc.Rendering;
using HomERP.WebUI.ModelMapping;
using HomERP.WebUI.Models.CashAccount;
using HomERP.WebUI.Models.Shared;

namespace HomERP.WebUI.Handlers
{
    public class CashAccountHandler : ICashAccountHandler
    {
        private ICashAccountProvider provider;
        ISessionDataProvider sessionProvider;

        public CashAccountHandler(ICashAccountProvider provider, ISessionDataProvider sessionProvider)
        {
            this.provider = provider;
            this.sessionProvider = sessionProvider;
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<int> identifiers)
        {
            return await this.provider.DeleteRangeAsync(identifiers);
        }

        public CashAccountVM Edit(int id)
        {
            return this.provider.CashAccounts.FirstOrDefault(c => c.Id == id).ToViewModel();
        }

        public async Task<bool> EditAsync(CashAccountVM model)
        {
            return await this.provider.SaveCashAccountAsync(model.ToEntity(sessionProvider.Family));
        }

        public CashAccountListVM GetList(PageInfo pageInfo)
        {
            CashAccountListVM model = new CashAccountListVM();
            model.PageInfo = pageInfo;
            var accountList = provider.CashAccounts;
            model.PageInfo.TotalItems = accountList.Count();
            accountList = accountList.Skip(model.PageInfo.RowsToSkip).Take(model.PageInfo.PageSize);
            model.CashAccounts = accountList.Select(x => x.ToViewModel());
            return model;
        }

        public async Task<PageInfo> PerformDeletion(int[] id, int page)
        {
            var model = new PageInfo();
            model.CurrentPage = page;
            if (id.Count() == 0)
            {
                model.Message.Text = "Nie zaznaczono pozycji do skasowania.";
                model.Message.Type = "info";
            }
            else
            {
                bool result = await this.DeleteRangeAsync(id);
                if (result)
                {
                    model.Message.Text = "Zaznaczone pozycje zostały usunięte.";
                    model.Message.Type = "success";
                }
                else
                {
                    model.Message.Text = "Nie można usunąć niektórych zaznaczonych pozycji.";
                    model.Message.Type = "danger";
                }
            }
            return model;
        }
    }
}
