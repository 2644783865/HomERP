using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using HomERP.Domain.Entity;
using HomERP.Domain.Authentication;
using Microsoft.EntityFrameworkCore.Metadata;

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
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PlannedPayment> PlannedPayments { get; set; }
        public DbSet<Family> Families { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Family>()
                .HasMany(f => f.FamilyAccounts)
                .WithOne(acc => acc.Family)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Family>()
                .HasMany(f => f.FamilyContractors)
                .WithOne(contractor => contractor.Family)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Contractor>()
                .HasMany(cont => cont.Payments)
                .WithOne(payment => payment.Contractor)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<CashAccount>()
                .HasMany(acc => acc.Payments)
                .WithOne(payment => payment.CashAccount)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
