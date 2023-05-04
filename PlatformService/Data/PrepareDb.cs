using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformService.Data
{
    public static class PrepareDb
    {
        public static void SeedData(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();

                if (!context.Platforms.Any())
                {
                    Console.WriteLine("--> Seed Data");
                    context.Platforms.AddRange(
                        new Entities.Platform() { Cost = "Free", Name = "Dot Net", Publisher = "Microsoft" },
                        new Entities.Platform() { Cost = "Free", Name = "Java", Publisher = "Oracle" },
                        new Entities.Platform() { Cost = "Free", Name = "Sql Server", Publisher = "Microsoft" }
                    );

                    context.SaveChanges();
                }
            }
        }
    }
}