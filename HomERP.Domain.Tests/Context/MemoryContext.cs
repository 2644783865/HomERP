using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using HomERP.Domain.Repository.Abstract;
using HomERP.Domain.Repository.EntityFramework;

namespace HomERP.Domain.Tests.Context
{
    class MemoryContext
    {
        public static DbContextOptions<EfDbContext> GenerateContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();
            var builder = new DbContextOptionsBuilder<EfDbContext>()
                .UseInMemoryDatabase()
                .UseInternalServiceProvider(serviceProvider);
            return builder.Options;
        }
    }
}
