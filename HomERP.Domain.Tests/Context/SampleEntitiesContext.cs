using HomERP.Domain.Entity;
using HomERP.Domain.Repository.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomERP.Domain.Tests.Context
{
    class SampleEntities
    {
        private static EfDbContext context;

        public static EfDbContext Context
        {
            get
            {
                GenerateContext();
                return context;
            }
        }

        private static void GenerateContext()
        {
            context = new EfDbContext(HomERP.Domain.Tests.Context.MemoryContext.GenerateContextOptions());
            context.Families.AddRange(new Family[]
            {
                new Family { Name = "Rodzinka" },
                new Family { Name = " Druga rodzinka" },
            });
            context.SaveChanges();
            Family myFamily = context.Families.First();
            Family otherFamily = context.Families.Skip(1).First();

            context.CashAccounts.AddRange(new CashAccount[]
            {
                new CashAccount { Family = myFamily, Name = "Konto", InitialAmount = 10 },
                new CashAccount { Family = myFamily, Name = "Konto2", InitialAmount = 20 },
                new CashAccount { Family = otherFamily, Name = "Konto obcej rodziny", InitialAmount = 30 }
            });
            context.SaveChanges();

            CashAccount account = context.CashAccounts.First();

            context.Payments.AddRange(new Payment[]
            {
                new Payment { CashAccount = account, Amount = 10 }
            });
            context.SaveChanges();
        }
    }
}
