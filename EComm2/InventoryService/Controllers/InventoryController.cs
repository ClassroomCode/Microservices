using ECommService.Data;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Controllers;

[ApiController]
public class InventoryController : ControllerBase
{
    private readonly IInventoryRepository _repository;
    private readonly ILogger<InventoryController> _logger;

    public InventoryController(IInventoryRepository repository, ILogger<InventoryController> logger)
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

    [HttpGet("inventory/{id}")]
    public async Task<ActionResult<int>> GetProductInventory(int id)
    {
        var retVal = await _repository.GetInventoryAsync(id);

        return Ok(retVal);
    }

    [HttpGet("inventory/reduce/{id}/{count}")]
    public async Task<ActionResult<int>> ReduceProductInventory(int id, int count)
    {
        return StatusCode(500);

        var newCount = await _repository.ReduceInventoryAsync(id, count);

        if (newCount == -1) return BadRequest();

        return Ok(newCount);
    }
}