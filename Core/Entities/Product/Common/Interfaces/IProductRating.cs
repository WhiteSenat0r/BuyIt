using Core.Common.Interfaces;

namespace Core.Entities.Product.Common.Interfaces;

public interface IProductRating : IEntity<Guid>
{
    double? Score { get; set; }
    
    Product Product { get; set; }
}