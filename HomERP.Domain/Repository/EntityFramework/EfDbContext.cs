using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HomERP.Domain.Entity;

namespace HomERP.Domain.Repository.EntityFramework
{
    class EfDbContext : DbContext
    {
        public EfDbContext() : base("Data Source = 192.168.1.20; Initial Catalog = HomERP; Integrated Security = True; MultipleActiveResultSets=True") { }
        public EfDbContext(string ConnectionString) : base(ConnectionString) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PlannedPayment> PlannedPayments { get; set; }
    }
}
