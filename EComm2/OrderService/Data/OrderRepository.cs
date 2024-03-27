using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OrderService.Data.Entities;

namespace ECommService.Data;

public class OrderRepository : IOrderRepository
{
    private readonly OrderDbContext _context;

    public OrderRepository(OrderDbContext context)
    {
        _context = context;
    }

    public async Task<Order?> GetOrderAsync(int id) =>
        await _context.Orders.SingleOrDefaultAsync(c => c.Id == id);

    public async Task AddOrderAsync(Order order)
    {


        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }
}
