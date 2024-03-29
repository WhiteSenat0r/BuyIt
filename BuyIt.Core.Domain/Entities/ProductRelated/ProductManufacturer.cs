﻿using System.ComponentModel.DataAnnotations;
using Domain.Contracts.ProductRelated;

namespace Domain.Entities.ProductRelated;

public sealed class ProductManufacturer : IProductManufacturer
{
    private string _name = null!;

    public ProductManufacturer() { } // Required by EF Core for object's initialization from database
    
    public ProductManufacturer(string name) => Name = name; // Typically used in non-database initialization

    public Guid Id { get; set; } = Guid.NewGuid();
    
    public ICollection<Product> Products { get; set; } = new List<Product>();

    [MaxLength(32)]
    public string Name
    {
        get => _name;
        set => AssignStringValue
            (value, ref _name);
    }
    
    private void AssignStringValue(string text, ref string assignedVariable)
    {
        if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
            throw new ArgumentNullException
            ("String is null, empty or consists only of white spaces!",
                new InvalidDataException());
        assignedVariable = text;
    }
}