using ECommService.Data.Entities;

namespace ECommService.Data;

public interface IRepository
{
    Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken token);

    Task<Product?> GetProductAsync(int id, CancellationToken token);
}