
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

    public async Task<int> ReduceInventoryAsync(int productId, int count)
    {
        var response = await _httpClient.GetAsync($"inventory/reduce/{productId}/{count}");

        if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) {
            return -1;
        }

        return await response.Content.ReadFromJsonAsync<int>();
    }
}
