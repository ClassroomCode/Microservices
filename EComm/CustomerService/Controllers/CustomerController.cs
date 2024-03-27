using CustomerService.Data;
using CustomerService.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Controllers;

[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerRepository _repository;
    private readonly ILogger<CustomerController> _logger;

    public CustomerController(ICustomerRepository repository, ILogger<CustomerController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet("customer/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Customer>> GetCustomer(int id, CancellationToken token)
    {
        var customer = await _repository.GetCustomerAsync(id, token);
        if (customer == null) return NotFound();
        return customer;
    }

    [HttpPost("customer")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> AddCustomer(CustomerRequest newCustomer, CancellationToken token)
    {
        var customer = await _repository.AddCustomerAsync(newCustomer.Name, newCustomer.PostalCode, token);

        return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
    }
}
