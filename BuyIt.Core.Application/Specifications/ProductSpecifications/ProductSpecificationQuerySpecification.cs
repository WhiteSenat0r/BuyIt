using System.Linq.Expressions;
using Application.Specifications.Common;
using Domain.Entities.ProductRelated;

namespace Application.Specifications.ProductSpecifications;

public sealed class ProductSpecificationQuerySpecification : QuerySpecification<ProductSpecification>
{
    public ProductSpecificationQuerySpecification()
    {
        AddInclude("SpecificationCategory");
        AddInclude("SpecificationAttribute");
        AddInclude("SpecificationValue");
    }
    
    public ProductSpecificationQuerySpecification(Expression<Func<ProductSpecification, bool>> criteria)
        : base(criteria)
    {
        AddInclude("SpecificationCategory");
        AddInclude("SpecificationAttribute");
        AddInclude("SpecificationValue");
    }
}