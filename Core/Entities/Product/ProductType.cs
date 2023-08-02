﻿using System.ComponentModel.DataAnnotations;
using Core.Entities.Product.Common.Interfaces;

namespace Core.Entities.Product;

public class ProductType : IProductType
{
    private string _name = null!;

    public ProductType() { }
    
    public ProductType(Product product, string name)
    {
        Product = product;
        Name = name;
    }
    
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [MaxLength(32)]
    public string Name
    {
        get => _name;
        set => AssignStringValue
            (value, ref _name);
    }

    public Product Product { get; set; } = null!;

    private static void AssignStringValue(string text, ref string assignedVariable)
    {
        if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
            throw new ArgumentNullException
            ("String is null, empty or consists only of white spaces!",
                new InvalidDataException());
        assignedVariable = text;
    }
}