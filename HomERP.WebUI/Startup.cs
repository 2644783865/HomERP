﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

using HomERP.Domain.Logic.Abstract;
using HomERP.Domain.Logic;
using HomERP.Domain.Repository.Abstract;
using HomERP.Domain.Repository.EntityFramework;

namespace HomERP.WebUI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Domain.Repository.EntityFramework.EfDbContext>(options => options.UseSqlServer("Server=localhost;Database=HomERP;Trusted_Connection=True;"));

            // Add framework services.
            services.AddMvc();

            services
                .AddTransient<IPaymentProvider, PaymentProvider>()
                .AddTransient<IPaymentRepository, EfPaymentRepository>()
                .AddTransient<IUserProvider, UserProvider>()
                .AddTransient<IUserRepository, EfUserRepository>()
                .AddTransient<IAccountProvider, AccountProvider>()
                .AddTransient<IAccountRepository, EfAccountRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}