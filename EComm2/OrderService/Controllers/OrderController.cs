using ECommService.Data;
using Microsoft.AspNetCore.Mvc;
using OrderService.Data.Entities;
using OrderService.ServiceClients;

namespace OrderService.Controllers;

[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository _repository;
    private readonly IInventoryServiceClient _inventoryServiceClient;
    private readonly ILogger<OrderController> _logger;

    public OrderController(IOrderRepository repository, 
        IInventoryServiceClient inventoryServiceClient,
        ILogger<OrderController> logger)
    {
        _repository = repository;
        _inventoryServiceClient = inventoryServiceClient;
        _logger = logger;
    }

    [HttpGet("order/{id}")]
    public async Task<ActionResult<Order>> GetOrder(int id)
    {
        var customer = await _repository.GetOrderAsync(id);

        if (customer == null) return NotFound();

        return Ok(customer);
    }

    [HttpPost("order")]
    public async Task<ActionResult<Order>> AddOrder(Order order)
    {
        int r = await _inventoryServiceClient.ReduceInventoryAsync(order.ProductId, order.Quantity);

        if (r >= 0) {
            await _repository.AddOrderAsync(order);
        }

        return CreatedAtAction("GetOrder", new { id = order.Id }, order);
    }
}
