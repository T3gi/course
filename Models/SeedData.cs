using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Phoenix.Data;
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
                        Mark = "Mustang",
                        Price = 9999
                    },
                    new Car
                    {
                        Name = "Chevrolet Camaro 2024",
                        ReleaseDate = DateTime.Parse("2024-12-8"),
                        Mark = "Camaro",
                        Price = 99999
                    },
                    new Car
                    {
                        Name = "Porsche Cayenne 2023",
                        ReleaseDate = DateTime.Parse("2023-5-11"),
                        Mark = "Cayenne",
                        Price = 999999
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
