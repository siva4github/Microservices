using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProduction
        )
        {
            using (var servicescope = app.ApplicationServices.CreateScope())
            {
                SeedData(servicescope.ServiceProvider.GetService<AppDbContext>(),isProduction);
            }
        }

        private static void SeedData(AppDbContext context, bool isProduction)
        {
            if (isProduction)
            {
                try
                {
                    Console.WriteLine("--> Applying migrations ...");
                    context.Database.Migrate();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"--> Could not run migtation {e.Message}");
                }
            }

            if (!context.Platforms.Any())
            {
                Console.WriteLine("--> Seed Data...");
                context.Platforms.AddRange(
                    new Platform()
                    {
                        Name = "Dot Net",
                        Publisher = "Microsoft",
                        Cost = "Free"
                    },
                    new Platform()
                    {
                        Name = "Sql Server Express",
                        Publisher = "Microsoft",
                        Cost = "Free"
                    },
                    new Platform()
                    {
                        Name = "Kubernetes",
                        Publisher = "Cloud Native Computing Foundation",
                        Cost = "Free"
                    });

                context.SaveChanges();
            }
        }
    }
}
