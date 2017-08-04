using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using HomERP.Domain.Entity;

namespace HomERP.Domain.Repository.EntityFramework
{
    public class EfDbContext : DbContext
    {
        public EfDbContext(DbContextOptions<EfDbContext> options) : base(options) { }

        public DbSet<CashAccount> CashAccounts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PlannedPayment> PlannedPayments { get; set; }
    }
}
