using Application.Specifications.Common;
using Domain.Entities;

namespace Application.Specifications;

public sealed class ProductTypeQueryByNameSpecification : QuerySpecification<ProductType>
{
    public ProductTypeQueryByNameSpecification(string name) 
        : base (criteria => criteria.Name.ToLower().Equals(name.ToLower())) { }
}