using ECommService.Data.Entities;

namespace ECommService.Data;

public class Repository : IRepository
{
    public async Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task<Product?> GetProductAsync(int id, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}
