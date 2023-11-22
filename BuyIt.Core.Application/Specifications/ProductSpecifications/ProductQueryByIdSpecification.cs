namespace Application.Specifications.ProductSpecifications;

public sealed class ProductQueryByIdSpecification : BasicProductQuerySpecification
{
    public ProductQueryByIdSpecification(Guid productId) 
        : base(criteria => criteria.Id == productId) => AddOrderByAscending(p => p.Name);
}