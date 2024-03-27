using CustomerService.Data.Entities;

namespace ECommService.Data;

public interface ICustomerRepository
{
    Task<Customer?> GetCustomerAsync(int id);

    Task AddCustomerAsync(Customer customer);
}
