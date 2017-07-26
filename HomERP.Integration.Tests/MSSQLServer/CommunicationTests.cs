using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;

using HomERP.Domain.Repository.EntityFramework;
using HomERP.Domain.Entity;

namespace HomERP.Integration.Tests.MSSQLServer
{
    [TestClass]
    public class CommunicationTests
    {
        private string databaseName = "HomERP";
        [TestMethod]
        public void ReadFromMSSQLServer()
        {
            //arrange
            var builder = new DbContextOptionsBuilder<EfDbContext>()
                .UseSqlServer("Server=localhost;Database=" + databaseName + "; Trusted_Connection=True;");
            EfDbContext context = new EfDbContext(builder.Options);
            int count = -1;
            //act
            count = context.Payments.Count();
            //assert
            count.Should().BeGreaterOrEqualTo(0);
        }

        [Ignore]
        [TestMethod]
        public void AddRecordsToMSSQLServer()
        {
            //arrange
            var builder = new DbContextOptionsBuilder<EfDbContext>()
                .UseSqlServer("Server=localhost;Database=" + databaseName + "; Trusted_Connection=True;");
            EfDbContext context = new EfDbContext(builder.Options);

            User user = new User { Email = "marcin@homerp.pl", Name = "Marcin" };
            Account account = new Account { InitialAmount = 10, Name = "Portfel" };
            Payment payment = new Payment
            {
                Account = account,
                Amount = 100,
                Direction = Domain.Helpers.CashFlowDirection.Increase,
                Time = DateTime.Now,
                User = user
            };

            context.Payments.Add(payment);
            context.SaveChanges();
        }
    }
}
