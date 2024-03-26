using ECommService.Data.Entities;

namespace ECommService.Data;

public interface IRepository
{
    Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken token);

    Task<Product?> GetProductAsync(int id, CancellationToken token);

    Task<Customer?> GetCustomerAsync(int id, CancellationToken token);

    Task<Customer> AddCustomerAsync(string name, string? postalCode, CancellationToken token);

    Task<bool> DeleteCustomerAsync(int id, CancellationToken token);

    Task<bool> UpdateCustomerPostalCodeAsync(int id, string postalCode, CancellationToken token);

    Task<Order?> GetOrderAsync(int id, CancellationToken token);

    Task<Order> AddOrderAsync(Customer customer, Product product, int quantity, double shippingCost, CancellationToken token);

    Task AddLoyaltyPointToCustomerAsync(Customer customer, CancellationToken token);

    Task ShipOrderAsync(Order order, CancellationToken token);
}