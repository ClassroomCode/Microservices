using CustomerService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommService.Data;

public class CustomerRepository : ICustomerRepository
{
    private readonly CustomerDbContext _context;

    public CustomerRepository(CustomerDbContext context)
    {
        _context = context;
    }

    public async Task<Customer?> GetCustomerAsync(int id) =>
        await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);

    public async Task AddCustomerAsync(Customer customer)
    {
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();
    }
}
