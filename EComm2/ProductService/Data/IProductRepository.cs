using ProductService.Data.Entities;

namespace ECommService.Data;

public interface IProductRepository
{
    Task AddSeedDataAsync();

    Task<Product[]> GetProductsAsync();
}
