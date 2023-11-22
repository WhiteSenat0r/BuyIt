using Domain.Contracts.Common;

namespace Domain.Contracts.ProductRelated;

public interface IProductRating : IEntity<Guid>
{
    double? Score { get; set; }
}