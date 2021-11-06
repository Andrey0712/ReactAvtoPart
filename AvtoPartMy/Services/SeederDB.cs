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
                //if (!context.Products.Any())
                //{
                //    //var prod = new Product
                //    //{
                //    //    Name = "мед",
                //    //    Price = 200,
                //    //    Photo = "https://storage1b.censor.net/images/b/1/4/e/b14e192c4f2065a59f3b98efaf4a937e/original.jpg"
                //    //};
                //    context.Products.Add(prod);
                //    context.SaveChanges();
                //}

                if (!context.Products.Any())
                {
                    var prods = new List<Product>
                                {
                                    new Product
                                        {
                                            Name = "мед",
                                            Price = 200,
                                            Photo = "https://storage1b.censor.net/images/b/1/4/e/b14e192c4f2065a59f3b98efaf4a937e/original.jpg"
                                        },
                                    new Product
                                        {
                                            Name = "сало",
                                            Price = 100,
                                            Photo = "https://vesti.ua/wp-content/uploads/2020/06/kak-zasolit-salo-528x352.jpg"
                                        },
                                    new Product
                                        {
                                            Name = "рыба",
                                            Price = 300,
                                            Photo = "https://kh-news.net/media/k2/items/src/3978ec5605c1f9eee2acef9b6f315dfb.jpg"
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
