using Domain.Contracts.Common;

namespace Domain.Contracts.ProductRelated;

public interface ISpecificationAspect : IEntity<Guid>
{ 
    string Value { get; }
}