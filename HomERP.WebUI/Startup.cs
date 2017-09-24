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
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using HomERP.Domain.Authentication;
using HomERP.Domain.Logic.Abstract;
using HomERP.Domain.Logic;
using HomERP.Domain.Repository.Abstract;
using HomERP.Domain.Repository.EntityFramework;
using HomERP.Domain.Services;

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

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<EfDbContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<HomERP.Domain.Authentication.Helpers.PolishIdentityErrorDescriber>();

            // Add framework services.
            services.AddMvc();

            // Configure Identity
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;

                // Cookie settings
                options.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromDays(150);
                options.Cookies.ApplicationCookie.LoginPath = "/Account/LogIn";
                options.Cookies.ApplicationCookie.LogoutPath = "/Account/LogOut";

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services
                .AddTransient<IPaymentProvider, PaymentProvider>()
                .AddTransient<IPaymentRepository, EfPaymentRepository>()
                .AddTransient<IFamilyUserProvider, FamilyUserProvider>()
                .AddTransient<IFamilyUserRepository, EfFamilyUserRepository>()
                .AddTransient<ICashAccountProvider, CashAccountProvider>()
                .AddTransient<ICashAccountRepository, EfCashAccountRepository>()
                .AddTransient<IFamilyProvider, FamilyProvider>()
                .AddTransient<IFamilyRepository, EfFamilyRepository>()
                .AddTransient<IUserProvider, UserProvider>()
                .AddTransient<IUserRepository, UserRepository>();

            services
                .AddTransient<IEmailSender, AuthMessageSender>()
                .AddTransient<ISmsSender, AuthMessageSender>();
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

            app.UseIdentity();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            Domain.Authentication.Helpers.RolesData.SeedRoles(app.ApplicationServices).Wait();
        }
    }
}
