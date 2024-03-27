using ECommService.Data;
using Microsoft.AspNetCore.Mvc;
using OrderService.Data.Entities;

namespace OrderService.Controllers;

[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository _repository;
    private readonly ILogger<OrderController> _logger;

    public OrderController(IOrderRepository repository, 
        ILogger<OrderController> logger)
    {
        _repository = repository;
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
        try {
            // TODO: Reduce inventory

            await _repository.AddOrderAsync(order);
            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        } catch {
            return StatusCode(500, "Bad Things");
        }
    }
}
