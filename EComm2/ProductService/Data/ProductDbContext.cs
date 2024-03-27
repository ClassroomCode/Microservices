using Microsoft.EntityFrameworkCore;
using ProductService.Data.Entities;

namespace ECommService.Data;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; } = null!;
}
