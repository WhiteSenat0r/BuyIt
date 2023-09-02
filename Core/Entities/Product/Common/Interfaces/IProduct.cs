using Core.Common.Interfaces;

namespace Core.Entities.Product.Common.Interfaces;

public interface IProduct : IEntity<Guid>
{
    string Name { get; }
    
    string Description { get; }
    
    decimal Price { get; set; }
    
    bool InStock { get; }
    
    ProductManufacturer Manufacturer { get; } 
    
    Guid ManufacturerId { get; }
    
    ProductRating Rating { get; }
    
    Guid RatingId { get; }
    
    ProductType ProductType { get; }
    
    Guid ProductTypeId { get; }
    
    string ProductCode { get; }
    
    IEnumerable<string> MainImagesNames { get; }

    IEnumerable<ProductSpecification> Specifications { get; }
}