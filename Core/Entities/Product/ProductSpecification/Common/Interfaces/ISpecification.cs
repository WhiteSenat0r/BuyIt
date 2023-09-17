using Core.Common.Interfaces;

namespace Core.Entities.Product.ProductSpecification.Common.Interfaces;

public interface ISpecificationAspect : IEntity<Guid>
{ 
    string Value { get; }
}