using InventoryService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommService.Data;

public class InventoryRepository : IInventoryRepository
{
    private readonly InventoryDbContext _context;

    public InventoryRepository(InventoryDbContext context)
    {
        _context = context;
    }

    public async Task AddSeedDataAsync()
    {
        await _context.Products.AddRangeAsync
        (
            new Product { Id = 1, NumberInStock = 14 },
            new Product { Id = 2, NumberInStock = 8 },
            new Product { Id = 3, NumberInStock = 0 }
        );
        await _context.SaveChangesAsync();
    }

    public async Task<int> GetInventoryAsync(int productId) =>
        await _context.Products
            .Where(p => p.Id == productId)
            .Select(p => p.NumberInStock)
            .SingleOrDefaultAsync();

    public async Task<int> ReduceInventoryAsync(int productId, int count)
    {
        var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == productId);

        if (product == null) return -1;

        if (product.NumberInStock - count < 0) return -1;

        product.NumberInStock -= count;

        await _context.SaveChangesAsync();

        return product.NumberInStock;
    }
}
