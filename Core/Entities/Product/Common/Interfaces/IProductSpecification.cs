using Core.Common.Interfaces;
using Core.Entities.Product.ProductSpecification;

namespace Core.Entities.Product.Common.Interfaces;

public interface IProductSpecification : IEntity<Guid>
{
    Guid SpecificationCategoryId { get; }
    
    ProductSpecificationCategory SpecificationCategory { get; }
    
    Guid SpecificationAttributeId { get; }
    
    ProductSpecificationAttribute SpecificationAttribute { get; }
    
    Guid SpecificationValueId { get; }
    
    ProductSpecificationValue SpecificationValue { get; }
}