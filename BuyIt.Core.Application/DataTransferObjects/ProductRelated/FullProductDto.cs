using Application.Contracts;

namespace Application.DataTransferObjects.ProductRelated;

public class FullProductDto : IProductDto
{
    public string Name { get; set; } = null!;
    
    public string Description { get; set; } = null!;

    public string Category { get; set; } = null!;
    
    public decimal Price { get; set; }
    
    public string InStock { get; set; } = null!;
    
    public string Brand { get; set; } = null!;
    
    public double? Rating { get; set; }

    public string ProductCode { get; set; } = null!;
    
    public IEnumerable<string> Images { get; set; } = null!;

    public IDictionary<string, IDictionary<string, string>> Specifications { get; set; } = null!;
}