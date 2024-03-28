namespace OrderService.ServiceClients;

public interface IInventoryServiceClient
{
    Task<int> ReduceInventoryAsync(int productId, int count);
}
