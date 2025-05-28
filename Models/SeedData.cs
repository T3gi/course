using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Phoenix.Areas.Identity.Data;
using System;
using System.Linq;

namespace Phoenix.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new PhoenixContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<PhoenixContext>>()))
            {
                if (context.Car.Any())
                {
                    return;
                }
                context.Car.AddRange(
                    new Car
                    {
                        Name = "Ford Mustang 1975",
                        ReleaseDate = DateTime.Parse("1975-5-15"),
                        Brand = "Ford",
                        Mark = "Mustang",
                        TechSpecs = "Diesel engine",
                        Price = 9999
                    },
                    new Car
                    {
                        Name = "Chevrolet Camaro 2024",
                        ReleaseDate = DateTime.Parse("2024-12-8"),
                        Brand = "Chevrolet",
                        Mark = "Camaro",
                        TechSpecs = "Gasoline engine",
                        Price = 99999
                    },
                    new Car
                    {
                        Name = "Porsche Cayenne 2023",
                        ReleaseDate = DateTime.Parse("2023-5-11"),
                        Brand = "Porsche",
                        Mark = "Cayenne",
                        TechSpecs = "Gasoline engine",
                        Price = 999999
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
