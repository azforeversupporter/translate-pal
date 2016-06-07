using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TranslatePal.Data.SqlServer
{
    public static class SampleData
    {
        public static async Task InitializeDatabaseAsync(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                await db.Database.MigrateAsync();
                
                if (!await db.Applications.AnyAsync())
                {
                    var application = new Application
                    {
                        Name = "DummyApplicationV1",
                        DisplayName = "Dummy Application",
                        AvailableLanguages = new List<string>() { "NL", "EN" },
                        DefaultLanguage = "EN"
                    };
                    db.Applications.Add(application);
                    await db.SaveChangesAsync();

                    var bundle = new Bundle
                    {
                        Name = "Messages",
                        ApplicationId = application.Id
                    };
                    db.Bundles.Add(bundle);
                    await db.SaveChangesAsync();

                    var loginElement = new Element
                    {
                        BundleId = bundle.Id,
                        ElementName = "header.control.button.login.title"
                    };
                    var logoutElement = new Element
                    {
                        BundleId = bundle.Id,
                        ElementName = "header.control.button.logout.title"
                    };
                    db.Elements.AddRange(loginElement, logoutElement);
                    await db.SaveChangesAsync();

                    var elementResources = new List<Resource>()
                    {
                        new Resource
                        {
                            ElementId = loginElement.Id,
                            Language = "NL",
                            Translation = "Inloggen"
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
                    db.Resources.AddRange(elementResources);
                    await db.SaveChangesAsync();
                }
            }
        }
    }
}
