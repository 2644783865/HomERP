using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;

using HomERP.Domain.Repository.EntityFramework;

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
    }
}
