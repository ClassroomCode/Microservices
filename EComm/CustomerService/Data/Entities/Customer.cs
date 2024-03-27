﻿namespace CustomerService.Data.Entities;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? PostalCode { get; set; }
}
