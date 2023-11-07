using Domain.Contracts.Common;
using Domain.Entities;

namespace Domain.Contracts.ProductRelated;

public interface IProductType : IEntity<Guid>
{
    string Name { get; }
    
    public ICollection<Product> Products { get; set; }
}