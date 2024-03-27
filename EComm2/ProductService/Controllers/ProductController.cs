using ECommService.Data;
using Microsoft.AspNetCore.Mvc;
using ProductService.Data.Entities;

namespace ProductService.Controllers;

[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _repository;
    private readonly ILogger<ProductController> _logger;

    public ProductController(IProductRepository repository, ILogger<ProductController> logger)
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
}
