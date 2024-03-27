using InventoryService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommService.Data;

public class InventoryDbContext : DbContext
{
    public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; } = null!;
}
