namespace ProductService.Data.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double UnitPrice { get; set; }
}
