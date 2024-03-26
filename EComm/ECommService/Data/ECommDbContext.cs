using ECommService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommService.Data;

public class ECommDbContext : DbContext
{
    public ECommDbContext(DbContextOptions<ECommDbContext> options)
        : base(options) { }

    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;

    public void AddSeedData()
    {
        Products.AddRange(
            new Product { Id = 1, Name = "Bread", UnitPrice = 3.00, QuantityInStock = 14 },
            new Product { Id = 2, Name = "Milk", UnitPrice = 4.50, QuantityInStock = 8 },
            new Product { Id = 3, Name = "Eggs", UnitPrice = 5.00, QuantityInStock = 0 }
        );
        Customers.Add(
            new Customer { Id = 1, Name = "Bill Gates", PostalCode = "23456" }
        );
        SaveChanges();
    }
}
