using Microsoft.AspNetCore.Mvc;

namespace ShippingService.Controllers;

[ApiController]
public class ShippingController : ControllerBase
{
    private readonly ILogger<ShippingController> _logger;

    public ShippingController(ILogger<ShippingController> logger)
    {
        _logger = logger;
    }
}