namespace CustomerService.Data.Entities;

public class Customer
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = String.Empty;
    public string PostalCode { get; set; } = String.Empty;
}
