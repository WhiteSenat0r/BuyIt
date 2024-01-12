using Domain.Contracts.Common;
using Domain.Entities.ProductRelated;

namespace Domain.Contracts.ProductRelated;

public interface IProductRating : IEntity<Guid>
{
    double? Score { get; set; }
    
    ICollection<Product> Products { get; set; }
}