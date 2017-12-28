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

        private async Task<bool> DeleteRangeAsync(IEnumerable<int> identifiers)
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

        public async Task<Message> PerformDeletion(IEnumerable<int> identifiers)
        {
            Message model = new Message();
            if (identifiers.Count() == 0)
            {
                model.Text = "Nie zaznaczono pozycji do skasowania.";
                model.Type = "info";
            }
            else
            {
                bool result = await this.DeleteRangeAsync(identifiers);
                if (result)
                {
                    model.Text = "Zaznaczone pozycje zostały usunięte.";
                    model.Type = "success";
                }
                else
                {
                    model.Text = "Nie można usunąć niektórych zaznaczonych pozycji.";
                    model.Type = "danger";
                }
            }
            return model;
        }
    }
}
