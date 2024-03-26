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
}
