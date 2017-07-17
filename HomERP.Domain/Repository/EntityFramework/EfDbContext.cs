using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HomERP.Domain.Entity;

namespace HomERP.Domain.Repository.EntityFramework
{
    public class EfDbContext : DbContext
    {
        public EfDbContext() : base("Data Source = 192.168.1.20; Initial Catalog = HomERP; Integrated Security = True; MultipleActiveResultSets=True") { }
        public EfDbContext(string ConnectionString) : base(ConnectionString) { }
        public EfDbContext(System.Data.Common.DbConnection connection) : base(connection, true) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Payment> Payments { get; set; }
        //        public DbSet<PlannedPayment> PlannedPayments { get; set; }
    }
}
