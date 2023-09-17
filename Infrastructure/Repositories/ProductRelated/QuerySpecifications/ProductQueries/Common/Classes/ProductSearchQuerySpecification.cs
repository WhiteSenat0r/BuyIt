using System.Linq.Expressions;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.FilteringModels;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.Classes;

public sealed class ProductSearchQuerySpecification : BasicProductFilteringQuerySpecification
{
    public ProductSearchQuerySpecification(ProductSearchFilteringModel filteringModel) : base(filteringModel)
    {
        Criteria = Criteria.And(product =>
            string.IsNullOrEmpty(filteringModel.Text) || product.Name.ToLower()
                .Contains(filteringModel.Text.ToLower()) || product.ProductCode.ToLower()
                .Equals(filteringModel.Text.ToLower()) || product.ProductType.Name.ToLower()
                .Equals(filteringModel.Text.ToLower()) || product.Specifications.Any
                (s => s.SpecificationValue.Value.ToLower().Equals(filteringModel.Text.ToLower().Replace('_', ' '))));
    }
}