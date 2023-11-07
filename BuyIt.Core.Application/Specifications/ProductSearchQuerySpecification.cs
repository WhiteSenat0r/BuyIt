using System.Linq.Expressions;
using Application.FilteringModels;

namespace Application.Specifications;

public sealed class ProductSearchQuerySpecification : BasicProductFilteringQuerySpecification
{
    public ProductSearchQuerySpecification(ProductSearchFilteringModel filteringModel) : base(filteringModel)
    {
        Criteria = Criteria.And(product =>
            string.IsNullOrEmpty(filteringModel.Text) || product.Name.ToLower()
                .Contains(filteringModel.Text.ToLower()) || product.ProductCode.ToLower()
                .Equals(filteringModel.Text.ToLower()) || product.ProductType.Name.ToLower()
                .Equals(filteringModel.Text.ToLower()) || product.Specifications.Any
                (s => s.SpecificationValue.Value.ToLower().Equals(
                    filteringModel.Text.ToLower().Replace('_', ' '))));
    }
}