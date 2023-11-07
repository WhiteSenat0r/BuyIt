using Application.Specifications.Common;
using Domain.Entities;

namespace Application.Specifications;

public sealed class ProductTypeQueryByIdSpecification : QuerySpecification<ProductType>
{
    public ProductTypeQueryByIdSpecification(Guid productTypeId) 
        : base (criteria => criteria.Id == productTypeId) { }
}