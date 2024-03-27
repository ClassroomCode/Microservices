using Microsoft.EntityFrameworkCore;
using OrderService.Data.Entities;

namespace ECommService.Data;

public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options)
        : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; } = null!;
}
