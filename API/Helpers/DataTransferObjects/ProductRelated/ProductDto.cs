using API.Helpers.DataTransferObjects.ProductRelated.Common.Interfaces;

namespace API.Helpers.DataTransferObjects.ProductRelated;

public class ProductDto : IProductDto
{
    public string Name { get; set; } = null!;
    
    public string Description { get; set; } = null!;
    
    public decimal Price { get; set; }
    
    public string InStock { get; set; } = null!;
    
    public string Brand { get; set; } = null!;
    
    public string Rating { get; set; } = null!;
    
    public string Category { get; set; } = null!;
    
    public string ProductCode { get; set; } = null!;
    
    public IEnumerable<string> Images { get; set; } = null!;
}