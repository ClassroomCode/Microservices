using ECommService.Data;
using ECommService.Data.DTO;
using ECommService.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommService.Controllers;

[ApiController]
public class ServiceController : ControllerBase
{
    private readonly IRepository _repository;
    private readonly ILogger<ServiceController> _logger;

    public ServiceController(IRepository repository, ILogger<ServiceController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet("product")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IEnumerable<Product>> GetAllProducts(CancellationToken token)
    {
        /*
        try {
            _logger.LogInformation("Starting slow task");

            await Task.Delay(10000, token);

            _logger.LogInformation("Fisnihed slow task");
        } catch (TaskCanceledException ex) {
            _logger.LogInformation("Slow task cancelled");
        }
        return Array.Empty<Product>();
        */

        return await _repository.GetAllProductsAsync(token);
    }

    [HttpGet("product/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> GetProduct(int id, CancellationToken token)
    {
        var product = await _repository.GetProductAsync(id, token);
        if (product == null) return NotFound();

        return product;
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

    [HttpDelete("customer/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteCustomer(int id, CancellationToken token)
    {
        var r = await _repository.DeleteCustomerAsync(id, token);

        if (!r) return NotFound();

        return NoContent();
    }

    [HttpPatch("customer/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateCustmerPostalCode(int id, [FromBody] string postalCode, CancellationToken token)
    {
        var r = await _repository.UpdateCustomerPostalCodeAsync(id, postalCode, token);

        if (!r) return NotFound();

        return NoContent();
    }

    [HttpPost("shipping")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<double>> GetShippingCost(PendingOrderRequest order, CancellationToken token)
    {
        var customer = await _repository.GetCustomerAsync(order.CustomerId, token);
        if (customer == null) return BadRequest("Customer does not exist");
        if (customer.PostalCode == null) return BadRequest("Missing postal code for customer");
        var product = await _repository.GetProductAsync(order.ProductId, token);
        if (product == null) return BadRequest("Product does not exist");

        return CalculateShippingCost(product, order.Quantity, customer.PostalCode);
    }

    [HttpGet("order/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Order>> GetOrder(int id, CancellationToken token)
    {
        var order = await _repository.GetOrderAsync(id, token);
        if (order == null) return NotFound();
        return order;
    }

    [HttpPost("order")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> AddOrder(PendingOrderRequest orderRequest, CancellationToken token)
    {
        var customer = await _repository.GetCustomerAsync(orderRequest.CustomerId, token);
        if (customer == null) return BadRequest("Customer does not exist");
        if (customer.PostalCode == null) return BadRequest("Missing postal code for customer");
        var product = await _repository.GetProductAsync(orderRequest.ProductId, token);
        if (product == null) return BadRequest("Product does not exist");

        var shippingCost = CalculateShippingCost(product, orderRequest.Quantity, customer.PostalCode);

        var order = await _repository.AddOrderAsync(customer, product, orderRequest.Quantity, shippingCost, token);

        await _repository.AddLoyaltyPointToCustomerAsync(customer, token);

        return CreatedAtAction("GetOrder", new { id = order.Id }, order);
    }

    [HttpPatch("shipping/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> ShipOrder(int id, CancellationToken token)
    {
        var order = await _repository.GetOrderAsync(id, token);
        if (order == null) return NotFound();

        await _repository.ShipOrderAsync(order, token);

        return NoContent();
    }

    private double CalculateShippingCost(Product product, int quantity, string postalCode)
    {
        var i = int.Parse(postalCode.Substring(0, 1));
        return (product.UnitPrice / 10) * quantity + i;
    }
}
