using System.Linq;
using Microsoft.EntityFrameworkCore;
using GasStation.Domain.Entities;
using GasStation.Domain.Enums;
using BCrypt.Net;

namespace GasStation.Infrastructure.Data;

public static class DbInitializer
{
    public static void Initialize(GasStationDbContext context)
    {
        context.Database.Migrate();

        if (!context.Users.Any())
        {
            var adminUser = new User
            {
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                Role = Role.Admin
            };
            context.Users.Add(adminUser);
            context.SaveChanges();
        }

        if (!context.Fuels.Any())
        {
            var fuels = new[]
            {
                new Fuel { Name = "Gasolina Regular", Price = 3.50m, IsActive = true },
                new Fuel { Name = "Gasolina Super", Price = 4.20m, IsActive = true },
                new Fuel { Name = "Diesel", Price = 3.00m, IsActive = true }
            };

            context.Fuels.AddRange(fuels);
            context.SaveChanges();

            foreach (var fuel in fuels)
            {
                context.Inventories.Add(new Inventory { FuelId = fuel.Id, Stock = 1000m }); // Stock inicial de 1000 gal/litros
            }
            context.SaveChanges();
        }
    }
}
