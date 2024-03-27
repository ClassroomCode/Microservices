using CustomerService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Data;

public class CustomerDbContext : DbContext
{
    public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
        : base(options) { }

    public DbSet<Customer> Customers { get; set; } = null!;

    public void AddSeedData()
    {
        Customers.Add(
            new Customer { Id = 1, Name = "Bill Gates", PostalCode = "23456" }
        );
        SaveChanges();
    }
}
