using CustomerService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Data;

public class CustomerRepository : ICustomerRepository
{
    private readonly CustomerDbContext _context;
    private readonly ILogger<CustomerRepository> _logger;

    public CustomerRepository(CustomerDbContext context, ILogger<CustomerRepository> logger)
    {
        _context = context;
        _logger = logger;
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
}
