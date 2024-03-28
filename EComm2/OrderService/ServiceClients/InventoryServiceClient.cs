
namespace OrderService.ServiceClients;

public class InventoryServiceClient : IInventoryServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<InventoryServiceClient> _logger;

    public InventoryServiceClient(HttpClient httpClient, 
        ILogger<InventoryServiceClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public Task<int> ReduceInventoryAsync(int productId, int count)
    {
        throw new NotImplementedException();
    }
}
