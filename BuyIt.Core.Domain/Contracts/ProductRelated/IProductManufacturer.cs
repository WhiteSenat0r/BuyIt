using Domain.Contracts.Common;
using Domain.Entities.ProductRelated;

namespace Domain.Contracts.ProductRelated;

public interface IProductManufacturer : IEntity<Guid>
{
    string Name { get; }
    
    ICollection<Product> Products { get; set; }
}