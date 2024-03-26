using ECommService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommService.Data;

public class Repository : IRepository
{
    private readonly ECommDbContext _context;

    public Repository(ECommDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken token)
    {
        return await _context.Products.ToArrayAsync();
    }

    public async Task<Product?> GetProductAsync(int id, CancellationToken token)
    {
        return await _context.Products.SingleOrDefaultAsync(p => p.Id == id);
    }
}
