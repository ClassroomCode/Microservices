using ProductService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommService.Data;

public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext _context;

    public ProductRepository(ProductDbContext context)
    {
        _context = context;
    }

    public async Task AddSeedDataAsync()
    {
        await _context.Products.AddRangeAsync
        (
            new Product { Id = 1, Name = "Bread", UnitPrice = 3.00 },
            new Product { Id = 2, Name = "Milk", UnitPrice = 4.50 },
            new Product { Id = 3, Name = "Eggs", UnitPrice = 5.00 }
        );
        await _context.SaveChangesAsync();
    }

    public async Task<Product[]> GetProductsAsync() =>
        await _context.Products.ToArrayAsync();
}
