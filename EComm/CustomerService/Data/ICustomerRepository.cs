using CustomerService.Data.Entities;

namespace CustomerService.Data;

public interface ICustomerRepository
{
    Task<Customer?> GetCustomerAsync(int id);

    Task AddCustomerAsync(Customer customer);
}
