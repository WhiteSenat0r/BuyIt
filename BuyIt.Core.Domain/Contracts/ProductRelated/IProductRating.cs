using Domain.Contracts.Common;
using Domain.Entities;

namespace Domain.Contracts.ProductRelated;

public interface IProductRating : IEntity<Guid>
{
    double? Score { get; set; }
    
    Product Product { get; set; }
}