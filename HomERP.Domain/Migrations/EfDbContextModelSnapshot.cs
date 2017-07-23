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
    partial class EfDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HomERP.Domain.Entity.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("InitialAmount");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("HomERP.Domain.Entity.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AccountId");

                    b.Property<decimal>("Amount");

                    b.Property<int>("Direction");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<DateTime>("Time");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("UserId");

                    b.ToTable("Payments");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Payment");
                });

            modelBuilder.Entity("HomERP.Domain.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AccountId");

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("PasswordHash");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

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
                    b.HasOne("HomERP.Domain.Entity.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");

                    b.HasOne("HomERP.Domain.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("HomERP.Domain.Entity.User", b =>
                {
                    b.HasOne("HomERP.Domain.Entity.Account")
                        .WithMany("Owner")
                        .HasForeignKey("AccountId");
                });
        }
    }
}
