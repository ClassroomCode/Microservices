using CustomerService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommService.Data;

public class CustomerDbContext : DbContext
{
    public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; } = null!;
}
