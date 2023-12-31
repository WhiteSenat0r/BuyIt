﻿namespace Application.DataTransferObjects.ProductRelated.Specification;

public class FilterDto
{
    public int MinPrice { get; set; }
    
    public int MaxPrice { get; set; }

    public IDictionary<string, int> CountedBrands { get; set; } = null!;
    
    public IDictionary<string, int> CountedCategories { get; set; } = null!;
    
    public IDictionary<string, int> CountedSpecifications { get; set; } = null!;
}