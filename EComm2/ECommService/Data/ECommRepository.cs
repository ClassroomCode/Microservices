using ECommService.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ECommService.Data;

public class ECommRepository : IECommRepository
{
    private readonly ECommDbContext _context;

    public ECommRepository(ECommDbContext context)
    {
        _context = context;
    }

    public async Task AddSeedDataAsync()
    {
        await _context.Products.AddRangeAsync
        (
            new Product { Id = 1, Name = "Bread", UnitPrice = 3.00, NumberInStock = 14 },
            new Product { Id = 2, Name = "Milk", UnitPrice = 4.50, NumberInStock = 8 },
            new Product { Id = 3, Name = "Eggs", UnitPrice = 5.00, NumberInStock = 0 }
        );
        await _context.SaveChangesAsync();
    }

    public async Task<Product[]> GetProductsAsync() =>
        await _context.Products.ToArrayAsync();

    public async Task<int> GetInventoryAsync(int productId) =>
        await _context.Products
            .Where(p => p.Id == productId)
            .Select(p => p.NumberInStock)
            .SingleOrDefaultAsync();

    public async Task<Customer?> GetCustomerAsync(int id) =>
        await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);

    public async Task AddCustomerAsync(Customer customer)
    {
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();
    }

    public async Task<Order?> GetOrderAsync(int id) =>
        await _context.Orders.SingleOrDefaultAsync(c => c.Id == id);

    public async Task AddOrderAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ShipOrderAsync(int id)
    {
        var order = await _context.Orders.SingleOrDefaultAsync(o => o.Id == id);

        if (order == null) return false;

        if (order.Shipped == true) return false;

        order.Shipped = true;
        await _context.SaveChangesAsync();

        return true;
    }
}
