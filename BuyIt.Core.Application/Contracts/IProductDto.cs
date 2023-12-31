﻿namespace Application.Contracts;

public interface IProductDto
{
    string Name { get; }
    
    public string Category { get; }
    
    decimal Price { get; }
    
    string InStock { get; }

    double? Rating { get; }

    string ProductCode { get; }
    
    IEnumerable<string> Images { get; }
}