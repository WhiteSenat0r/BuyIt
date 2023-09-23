using Core.Common.Interfaces;

namespace Core.Entities.Product.Common.Interfaces;

public interface IProductType : IEntity<Guid>
{
    string Name { get; }
    
    public ICollection<Product> Products { get; set; }
}