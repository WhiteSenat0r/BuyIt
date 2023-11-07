using Application.Contracts;

namespace Application.DataTransferObjects.ProductRelated;

public sealed class GeneralizedProductDto : IProductDto
{
    public string Name { get; set; } = null!;
    
    public string Description { get; set; } = null!;
    
    public decimal Price { get; set; }
    
    public string InStock { get; set; } = null!;

    public double? Rating { get; set; }

    public string Category { get; set; } = null!;
    
    public string ProductCode { get; set; } = null!;
    
    public IEnumerable<string> Images { get; set; } = null!;
}