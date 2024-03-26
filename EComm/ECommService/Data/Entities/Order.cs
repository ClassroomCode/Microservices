namespace ECommService.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double? ShippingCost { get; set; }
        public bool HasShipped { get; set; }
    }
}
