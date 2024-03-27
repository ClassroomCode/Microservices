
namespace ECommService.Data.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string? PostalCode { get; set; }
        public int LoyaltyPoints { get; set; }
    }
}
