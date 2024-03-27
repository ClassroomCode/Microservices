using OrderService.Data.Entities;

namespace ECommService.Data;

public interface IOrderRepository
{
    Task<Order?> GetOrderAsync(int id);

    Task AddOrderAsync(Order order);
}
