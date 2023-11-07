using Domain.Contracts.Common;
using Domain.Entities;

namespace Domain.Contracts.ProductRelated;

public interface ISpecificationAspect : IEntity<Guid>
{ 
    string Value { get; }
    
    ICollection<ProductSpecification> Specifications { get; set; }
}