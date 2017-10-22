using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using HomERP.Domain.Repository.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace HomERP.Domain.Authentication.Helpers
{
    public static class RolesData
    {
        private static readonly string[] Roles = new string[] { "Administrator", "FamilyMember", "User"};

        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<EfDbContext>();

                if (!dbContext.UserRoles.Any())
                {
                    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                    foreach (var role in Roles)
                    {
                        if (!await roleManager.RoleExistsAsync(role))
                        {
                            await roleManager.CreateAsync(new IdentityRole(role));
                        }
                    }
                }

            }
        }
    }
}
