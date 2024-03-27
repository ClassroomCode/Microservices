using ECommService.Data.Entities;

namespace ECommService.Data;

public interface IECommRepository
{
    Task AddSeedDataAsync();

    Task<Product[]> GetProductsAsync();

    Task<int> GetInventoryAsync(int productId);

    Task<Customer?> GetCustomerAsync(int id);

    Task AddCustomerAsync(Customer customer);

    Task<Order?> GetOrderAsync(int id);

    Task AddOrderAsync(Order order);

    Task<bool> ShipOrderAsync(int id);
}
