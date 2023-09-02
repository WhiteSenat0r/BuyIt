using Core.Common.Interfaces;

namespace Core.Entities.Product.Common.Interfaces;

public interface IProductSpecification : IEntity<Guid>
{
    string Category { get; }
    
    string Attribute { get; }
    
    string Value { get; }
}