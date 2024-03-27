namespace ECommService.Data;

public interface IInventoryRepository
{
    Task AddSeedDataAsync();

    Task<int> GetInventoryAsync(int productId);

    Task<int> ReduceInventoryAsync(int productId, int count);
}
