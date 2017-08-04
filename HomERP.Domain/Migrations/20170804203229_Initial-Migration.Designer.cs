using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using HomERP.Domain.Repository.EntityFramework;
using HomERP.Domain.Helpers;

namespace HomERP.Domain.Migrations
{
    [DbContext(typeof(EfDbContext))]
    [Migration("20170804203229_Initial-Migration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HomERP.Domain.Entity.CashAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("InitialAmount");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("CashAccounts");
                });

            modelBuilder.Entity("HomERP.Domain.Entity.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<int?>("CashAccountId");

                    b.Property<int>("Direction");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<DateTime>("Time");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CashAccountId");

                    b.HasIndex("UserId");

                    b.ToTable("Payments");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Payment");
                });

            modelBuilder.Entity("HomERP.Domain.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("PasswordHash");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HomERP.Domain.Entity.PlannedPayment", b =>
                {
                    b.HasBaseType("HomERP.Domain.Entity.Payment");

                    b.Property<int>("Status");

                    b.ToTable("PlannedPayment");

                    b.HasDiscriminator().HasValue("PlannedPayment");
                });

            modelBuilder.Entity("HomERP.Domain.Entity.Payment", b =>
                {
                    b.HasOne("HomERP.Domain.Entity.CashAccount", "CashAccount")
                        .WithMany()
                        .HasForeignKey("CashAccountId");

                    b.HasOne("HomERP.Domain.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
        }
    }
}
