using ECommService.Data;
using ECommService.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommService.Controllers;

[ApiController]
public class ServiceController : ControllerBase
{
    private readonly IRepository _repository;

    public ServiceController(IRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("product")]
    public async Task<IEnumerable<Product>> GetAllProducts(CancellationToken token)
    {
        var products = await _repository.GetAllProductsAsync(token);
        return products;
    }

    [HttpGet("product/{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id, CancellationToken token)
    {
        var product = await _repository.GetProductAsync(id, token);

        if (product is null) return NotFound();

        return product;
    }
}
