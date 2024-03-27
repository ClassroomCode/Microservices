using ECommService.Data.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ECommService.Data;

public class Repository : IRepository
{
    private readonly ECommDbContext _context;
    private readonly ILogger<Repository> _logger;

    public Repository(ECommDbContext context, ILogger<Repository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken token)
    {
        return await _context.Products.ToArrayAsync(token);
    }

    public async Task<Product?> GetProductAsync(int id, CancellationToken token)
    {
        return await _context.Products.SingleOrDefaultAsync(p => p.Id == id, token);
    }

    public async Task<Customer?> GetCustomerAsync(int id, CancellationToken token)
    {
        return await _context.Customers.SingleOrDefaultAsync(c => c.Id == id, token);
    }

    public async Task<Customer> AddCustomerAsync(string name, string? postalCode, CancellationToken token)
    {
        var customer = new Customer { Name = name, PostalCode = postalCode };

        await _context.Customers.AddAsync(customer);

        await _context.SaveChangesAsync();

        return customer;
    }

    public async Task<bool> DeleteCustomerAsync(int id, CancellationToken token)
    {
        var customer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id, token);

        if (customer is not null) {
            _context.Customers.Remove(customer);
            int n = await _context.SaveChangesAsync();
            return (n > 0);
        }
        return false;
    }

    public async Task<bool> UpdateCustomerPostalCodeAsync(int id, string postalCode, CancellationToken token)
    {
        var customer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id, token);

        if (customer != null) {
            customer.PostalCode = postalCode;
            int n = await _context.SaveChangesAsync();
            return (n > 0);
        }
        return false;
    }

    public async Task<Order?> GetOrderAsync(int id, CancellationToken token)
    {
        return await _context.Orders.SingleOrDefaultAsync(o => o.Id == id, token);
    }

    public async Task<Order> AddOrderAsync(Customer customer, Product product, int quantity, double shippingCost, CancellationToken token)
    {
        var order = new Order {
            CustomerId = customer.Id,
            ProductId = product.Id,
            Quantity = quantity,
            ShippingCost = shippingCost
        };

        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();

        return order;
    }

    public async Task AddLoyaltyPointToCustomerAsync(Customer customer, CancellationToken token)
    {
        customer.LoyaltyPoints++;
        int n = await _context.SaveChangesAsync();
    }

    public async Task ShipOrderAsync(Order order, CancellationToken token)
    {
        order.HasShipped = true;
        await _context.SaveChangesAsync();
    }
}
