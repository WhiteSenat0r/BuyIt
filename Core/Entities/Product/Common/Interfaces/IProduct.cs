using Core.Common.Interfaces;

namespace Core.Entities.Product.Common.Interfaces;

public interface IProduct : IEntity<Guid>
{
    string Name { get; set; }
    
    string Description { get; set; }
    
    decimal Price { get; set; }
    
    bool InStock { get; set; }
    
    ProductManufacturer Manufacturer { get; set; } 
    
    Guid ManufacturerId { get; set; }
    
    ProductRating Rating { get; set; }
    
    Guid RatingId { get; set; }
    
    ProductType ProductType { get; set; }
    
    Guid ProductTypeId { get; set; }
    
    IDictionary<string, IEnumerable<string>> MainImagesUrls { get; set; }
    
    IEnumerable<string>? DescriptionImagesUrls { get; set; }
    
    IDictionary<string, IDictionary<string, string>> Specifications { get; set; }
}