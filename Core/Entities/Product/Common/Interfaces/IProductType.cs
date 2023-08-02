using Core.Common.Interfaces;

namespace Core.Entities.Product.Common.Interfaces;

public interface IProductType : IEntity<Guid>
{
    string Name { get; set; }
    
    Product Product { get; set; }
}