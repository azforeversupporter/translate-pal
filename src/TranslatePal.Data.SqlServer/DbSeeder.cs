using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TranslatePal.Data.SqlServer
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                await context.Database.MigrateAsync();
                await SeedUsersAndRolesAsync(serviceProvider, context);
                await SeedApplications(context);               
            }
        }

        private static async Task SeedUsersAndRolesAsync(IServiceProvider serviceProvider, ApplicationDbContext context)
        {
            const string userEmail = "test@test.com";
            const string userPassword = "P@ssw0rd!";
            const string userRoleName = "user";

            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetService<RoleManager<ApplicationRole>>();
            
            if (!await context.Users.AnyAsync())
            {
                var user = await userManager.FindByEmailAsync(userEmail);
                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        UserName = userEmail,
                        Email = userEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(user, userPassword);
                }

                if (await roleManager.FindByNameAsync(userRoleName) == null)
                {
                    await roleManager.CreateAsync(new ApplicationRole
                    {
                        Name = userRoleName
                    });
                }

                if (!await userManager.IsInRoleAsync(user, userRoleName))
                {
                    await userManager.AddToRoleAsync(user, userRoleName);
                }
            }
        }

        private static async Task SeedApplications(ApplicationDbContext context)
        {
            if (!await context.Apps.AnyAsync())
            {
                var application = new Application
                {
                    Name = "Dummy Application",
                    Languages = new List<string> { "NL", "EN" },
                    DefaultLanguage = "EN"
                };
                context.Apps.Add(application);
                await context.SaveChangesAsync();

                var loginElement = new Element
                {
                    ApplicationId = application.Id,
                    Name = "header.control.button.login.title"
                };
                var logoutElement = new Element
                {
                    ApplicationId = application.Id,
                    Name = "header.control.button.logout.title"
                };
                context.Elements.AddRange(loginElement, logoutElement);
                await context.SaveChangesAsync();

                var resources = new Resource[]
                {
                    new Resource
                    {
                        ElementId = loginElement.Id,
                        Language = "NL",
                        Translation = "Aanmelden"
                    },
                    new Resource
                    {
                        ElementId = loginElement.Id,
                        Language = "EN",
                        Translation = "Login"
                    },
                    new Resource 
                    {
                        ElementId = logoutElement.Id,
                        Language = "EN",
                        Translation = "Logoff"
                    }
                };
                context.Resources.AddRange(resources);
                await context.SaveChangesAsync();
            }
        }
    }
}