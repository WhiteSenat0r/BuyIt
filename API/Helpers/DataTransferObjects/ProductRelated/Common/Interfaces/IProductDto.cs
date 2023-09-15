namespace API.Helpers.DataTransferObjects.ProductRelated.Common.Interfaces;

public interface IProductDto
{
    string Name { get; }

    string Description { get; }
    
    public string Category { get; }
    
    decimal Price { get; }
    
    string InStock { get; }

    double? Rating { get; }

    string ProductCode { get; }
    
    IEnumerable<string> Images { get; }
}