using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace API.Data
{
    public static class AppDbInitializer
    {
        public static async Task Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<StoreContext>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                if (context == null)
                {
                    throw new ArgumentNullException(nameof(context));
                }

                if (userManager == null)
                {
                    throw new ArgumentNullException(nameof(userManager));
                }

                if (roleManager == null)
                {
                    throw new ArgumentNullException(nameof(roleManager));
                }

                try
                {
                    // Create roles first
                    if (!await roleManager.RoleExistsAsync("Member"))
                    {
                        await roleManager.CreateAsync(new IdentityRole("Member"));
                    }

                    if (!await roleManager.RoleExistsAsync("Admin"))
                    {
                        await roleManager.CreateAsync(new IdentityRole("Admin"));
                    }

                    // Create users
                    if (!userManager.Users.Any())
                    {
                        var memberUser = new User
                        {
                            UserName = "user",
                            Email = "user@company.com"
                        };

                        var result = await userManager.CreateAsync(memberUser, "Pa$$w0rd");
                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(memberUser, "Member");
                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                Console.WriteLine($"Error creating user: {error.Description}");
                            }
                        }

                        var adminUser = new User
                        {
                            UserName = "admin",
                            Email = "admin@company.com"
                        };

                        result = await userManager.CreateAsync(adminUser, "Pa$$w0rd");
                        if (result.Succeeded)
                        {
                            await userManager.AddToRolesAsync(adminUser, new[] { "Member", "Admin" });
                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                Console.WriteLine($"Error creating admin user: {error.Description}");
                            }
                        }
                    }

                    // Seed products
                    if (!context.Products.Any())
                    {
                        context.Products.AddRange(new List<Product>
                        {
                            new Product
                            {
                                Name = "Angular Speedster Board 2000",
                                Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                                Price = 20000,
                                PictureUrl = "/images/products/sb-ang1.png",
                                Brand = "Angular",
                                Type = "Boards",
                                QuantityInStock = 100
                            },
                            new Product
                            {
                                Name = "Green Angular Board 3000",
                                Description = "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.",
                                Price = 15000,
                                PictureUrl = "/images/products/sb-ang2.png",
                                Brand = "Angular",
                                Type = "Boards",
                                QuantityInStock = 100
                            },
                            new Product
                            {
                                Name = "Core Board Speed Rush 3",
                                Description = "Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy.",
                                Price = 18000,
                                PictureUrl = "/images/products/sb-core1.png",
                                Brand = "NetCore",
                                Type = "Boards",
                                QuantityInStock = 100
                            },
                            new Product
                            {
                                Name = "Net Core Super Board",
                                Description = "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.",
                                Price = 30000,
                                PictureUrl = "/images/products/sb-core2.png",
                                Brand = "NetCore",
                                Type = "Boards",
                                QuantityInStock = 100
                            },
                            new Product
                            {
                                Name = "React Board Super Whizzy Fast",
                                Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                                Price = 25000,
                                PictureUrl = "/images/products/sb-react1.png",
                                Brand = "React",
                                Type = "Boards",
                                QuantityInStock = 100
                            },
                            new Product
                            {
                                Name = "Typescript Entry Board",
                                Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                                Price = 12000,
                                PictureUrl = "/images/products/sb-ts1.png",
                                Brand = "TypeScript",
                                Type = "Boards",
                                QuantityInStock = 100
                            },
                            new Product
                            {
                                Name = "Core Blue Hat",
                                Description = "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                                Price = 1000,
                                PictureUrl = "/images/products/hat-core1.png",
                                Brand = "NetCore",
                                Type = "Hats",
                                QuantityInStock = 100
                            },
                            new Product
                            {
                                Name = "Green React Woolen Hat",
                                Description = "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                                Price = 8000,
                                PictureUrl = "/images/products/hat-react1.png",
                                Brand = "React",
                                Type = "Hats",
                                QuantityInStock = 100
                            },
                            new Product
                            {
                                Name = "Purple React Woolen Hat",
                                Description = "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                                Price = 1500,
                                PictureUrl = "/images/products/hat-react2.png",
                                Brand = "React",
                                Type = "Hats",
                                QuantityInStock = 100
                            },
                            new Product
                            {
                                Name = "Blue Code Gloves",
                                Description = "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                                Price = 1800,
                                PictureUrl = "/images/products/glove-code1.png",
                                Brand = "VS Code",
                                Type = "Gloves",
                                QuantityInStock = 100
                            },
                            new Product
                            {
                                Name = "Green Code Gloves",
                                Description = "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                                Price = 1500,
                                PictureUrl = "/images/products/glove-code2.png",
                                Brand = "VS Code",
                                Type = "Gloves",
                                QuantityInStock = 100
                            },
                            new Product
                            {
                                Name = "Purple React Gloves",
                                Description = "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                                Price = 1600,
                                PictureUrl = "/images/products/glove-react1.png",
                                Brand = "React",
                                Type = "Gloves",
                                QuantityInStock = 100
                            },
                            new Product
                            {
                                Name = "Green React Gloves",
                                Description = "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                                Price = 1400,
                                PictureUrl = "/images/products/glove-react2.png",
                                Brand = "React",
                                Type = "Gloves",
                                QuantityInStock = 100
                            },
                            new Product
                            {
                                Name = "Redis Red Boots",
                                Description = "Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy.",
                                Price = 25000,
                                PictureUrl = "/images/products/boot-redis1.png",
                                Brand = "Redis",
                                Type = "Boots",
                                QuantityInStock = 100
                            },
                            new Product
                            {
                                Name = "Core Red Boots",
                                Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                                Price = 18999,
                                PictureUrl = "/images/products/boot-core2.png",
                                Brand = "Redis",
                                Type = "Boots",
                                QuantityInStock = 100
                            },
                            new Product
                            {
                                Name = "Core Purple Boots",
                                Description = "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.",
                                Price = 19999,
                                PictureUrl = "/images/products/boot-core1.png",
                                Brand = "NetCore",
                                Type = "Boots",
                                QuantityInStock = 100
                            },
                            new Product
                            {
                                Name = "Angular Purple Boots",
                                Description = "Aenean nec lorem. In porttitor. Donec laoreet nonummy augue.",
                                Price = 15000,
                                PictureUrl = "/images/products/boot-ang2.png",
                                Brand = "Angular",
                                Type = "Boots",
                                QuantityInStock = 100
                            },
                            new Product
                            {
                                Name = "Angular Blue Boots",
                                Description = "Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy.",
                                Price = 18000,
                                PictureUrl = "/images/products/boot-ang1.png",
                                Brand = "Angular",
                                Type = "Boots",
                                QuantityInStock = 100
                            }
                        });

                        await context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
