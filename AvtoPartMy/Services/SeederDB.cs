using AvtoPartMy.Constans;
using Data;
using Data.Entities.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvtoPartMy.Services
{
    public static class SeederDB
    {
        public static void SeedData(this IApplicationBuilder app)
        {

            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var manager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var managerRole = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
                
                var result = managerRole.CreateAsync(new AppRole
                {
                    Name = Roles.Admin
                }).Result;

                result = managerRole.CreateAsync(new AppRole
                {
                    Name = Roles.User
                }).Result;

                string email = "admin@gmail.com";
                var user = new AppUser
                {
                    Email = email,
                    UserName = email,
                    PhoneNumber = "+11(111)111-11-11"
                };
                result = manager.CreateAsync(user, "Qwerty1-").Result;
                result = manager.AddToRoleAsync(user, Roles.Admin).Result;

                

            }
        }

        public static void SeedProd(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppEFContext>();
                

                if (!context.Products.Any())
                {
                    var prods = new List<Product>
                                {
                                    new Product
                                        {
                                            Name = "мед",
                                            Price = 200,
                                            Photo = "1.jpg"
                                        },
                                    new Product
                                        {
                                            Name = "сало",
                                            Price = 100,
                                            Photo = "2.jpg"
                                        },
                                    new Product
                                        {
                                            Name = "рыба",
                                            Price = 300,
                                            Photo = "3.jpg"
                                        },

                                };

                    foreach (var prod in prods)
                    {
                        context.Add(prod);
                        context.SaveChanges();
                    }
                }
            }
        }


    }
}
