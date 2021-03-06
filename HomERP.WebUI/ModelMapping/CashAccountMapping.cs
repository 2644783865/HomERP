﻿using HomERP.WebUI.Models.CashAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomERP.WebUI.ModelMapping
{
    public static class CashAccountMapping
    {
        public static CashAccountVM ToViewModel(this Domain.Entity.CashAccount account)
        {
            CashAccountVM viewModel = new CashAccountVM
            {
                Active = account.Active,
                Description = account.Description,
                Id = account.Id,
                InitialAmount = account.InitialAmount,
                Name = account.Name
            };
            return viewModel;
        }

        public static Domain.Entity.CashAccount ToEntity(this CashAccountVM accountVM, Domain.Entity.Family family = null)
        {
            Domain.Entity.CashAccount payment = new Domain.Entity.CashAccount
            {
                Active = accountVM.Active,
                Description = accountVM.Description,
                Id = accountVM.Id,
                InitialAmount = accountVM.InitialAmount,
                Family = family,
                Name = accountVM.Name
            };
            return payment;
        }

        public static CashAccountVM AddEntity(this CashAccountVM accountVM, Domain.Entity.CashAccount account)
        {
            accountVM.Active = account.Active;
            accountVM.Description = account.Description;
            accountVM.Id = account.Id;
            accountVM.InitialAmount = account.InitialAmount;
            accountVM.Name = account.Name;
            return accountVM;
        }
    }
}