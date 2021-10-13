using AvtoPartMy.Constans;
using Data.Entities.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvtoPartMy.Helper
{
    public static class AddAdminConfig
    {
        /// <summary>
        /// Create admin with password for access
        /// </summary>
        /// <param name="app"></param>
        public static void AdminConfig(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            var role = new AppRole
            {
                Name = Roles.Admin
            };
            var result1 = roleManager.CreateAsync(role).Result;

            var user = new AppUser
            {
                Email = "admin@gmail.com",
                UserName = "admin@gmail.com"
            };
            var result = userManager.CreateAsync(user, "admin77").Result;
            result = userManager.AddToRoleAsync(user, Roles.Admin).Result;


        }
    }
}
