using Core.Common.Interfaces;

namespace Core.Entities.Product.ProductSpecificationRelated.Common.Interfaces;

public interface ISpecificationAspect : IEntity<Guid>
{ 
    string Value { get; }
    
    ICollection<ProductSpecification> Specifications { get; set; }
}