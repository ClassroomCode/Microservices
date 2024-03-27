using ECommService.Data;
using ECommService.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ECommService.Controllers;

[ApiController]
public class ECommController : ControllerBase
{
    private readonly IECommRepository _repository;
    private readonly ILogger<ECommController> _logger;

    public ECommController(IECommRepository repository, ILogger<ECommController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet("seed")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Seed()
    {
        await _repository.AddSeedDataAsync();

        return NoContent();
    }

    [HttpGet("product")]
    public async Task<ActionResult<Product[]>> GetAllProducts()
    {
        var products = await _repository.GetProductsAsync();

        return products;
    }

    [HttpGet("inventory/{id}")]
    public async Task<ActionResult<int>> GetProductInventory(int id)
    {
        var retVal = await _repository.GetInventoryAsync(id);

        return Ok(retVal);
    }

    [HttpGet("customer/{id}")]
    public async Task<ActionResult<Customer>> GetCustomer(int id)
    {
        var customer = await _repository.GetCustomerAsync(id);

        if (customer == null) return NotFound();

        return Ok(customer);
    }

    [HttpPost("customer")]
    public async Task<ActionResult<Customer>> AddCustomer(Customer customer)
    {
        await _repository.AddCustomerAsync(customer);

        return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
    }

    [HttpGet("order/{id}")]
    public async Task<ActionResult<Order>> GetOrder(int id)
    {
        var customer = await _repository.GetCustomerAsync(id);

        if (customer == null) return NotFound();

        return Ok(customer);
    }

    [HttpPost("order")]
    public async Task<ActionResult<Order>> AddOrder(Order order)
    {
        await _repository.AddOrderAsync(order);

        return CreatedAtAction("GetOrder", new { id = order.Id }, order);
    }

    [HttpPatch("ship")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> ShipOrder(int id)
    {
        var b = await _repository.ShipOrderAsync(id);

        if (!b) return BadRequest();

        return NoContent();
    }
}
