using Application.Specifications.Common;
using Domain.Entities;

namespace Application.Specifications.ProductTypeSpecifications;

public sealed class ProductTypeQuerySpecification : QuerySpecification<ProductType>
{
    public ProductTypeQuerySpecification() => AddOrderByAscending(p => p.Name);
}