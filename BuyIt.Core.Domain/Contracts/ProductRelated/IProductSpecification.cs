using Domain.Contracts.Common;
using Domain.Entities;

namespace Domain.Contracts.ProductRelated;

public interface IProductSpecification : IEntity<Guid>
{
    Guid SpecificationCategoryId { get; }
    
    ProductSpecificationCategory SpecificationCategory { get; }
    
    Guid SpecificationAttributeId { get; }
    
    ProductSpecificationAttribute SpecificationAttribute { get; }
    
    Guid SpecificationValueId { get; }
    
    ProductSpecificationValue SpecificationValue { get; }
}