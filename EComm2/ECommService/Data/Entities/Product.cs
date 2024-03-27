﻿namespace ECommService.Data.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public double UnitPrice { get; set; }
    public int NumberInStock { get; set; }
}
