using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using HomERP.Domain.Entity;
using HomERP.Domain.Authentication;


namespace HomERP.Domain.Repository.EntityFramework
{
    public class EfDbContext : IdentityDbContext<ApplicationUser>
    {
        //parameterless constructor used for creating migrations
        public EfDbContext()
            : base(new DbContextOptionsBuilder()
                  .UseSqlServer("Server=localhost;Database=HomERP;Trusted_Connection=True;")
                  .Options)
        { }

        public EfDbContext(DbContextOptions<EfDbContext> options) : base(options) { }

        public DbSet<CashAccount> CashAccounts { get; set; }
        public DbSet<FamilyUser> FamilyUsers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PlannedPayment> PlannedPayments { get; set; }
        public DbSet<Family> Families { get; set; }
    }
}
