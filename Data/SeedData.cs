using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SpotOps.Api.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService
                    <DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Spots.Any())
                {
                    return;
                }

                context.Spots.AddRange(
                    new Models.Spot
                    {
                        Id = 1,
                        Name = "Denver Skatepark",
                        DateCreated = DateTime.Now,
                        Type = "Park"
                    },
                    new Models.Spot
                    {
                        Id = 2,
                        Name = "Federal Church Rail",
                        DateCreated = DateTime.Now,
                        Type = "Rail"
                    });

                context.SaveChanges();
            }
        }
    }
}