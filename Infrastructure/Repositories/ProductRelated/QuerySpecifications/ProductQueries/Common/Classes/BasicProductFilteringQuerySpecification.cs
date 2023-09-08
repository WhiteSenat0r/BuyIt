using System.Linq.Expressions;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common
    .FilteringModels.Common.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.Classes;

public abstract class BasicProductFilteringQuerySpecification : BasicProductQuerySpecification
{
    protected BasicProductFilteringQuerySpecification(IFilteringModel filteringModel)
        : base(product =>
            (string.IsNullOrEmpty(filteringModel.BrandName) || product.Manufacturer.Name.ToLower().Equals
                (filteringModel.BrandName.ToLower())) &&
            (!filteringModel.UpperPriceLimit.HasValue || product.Price <= filteringModel.UpperPriceLimit) &&
            (!filteringModel.LowerPriceLimit.HasValue || product.Price >= filteringModel.LowerPriceLimit) &&
            (filteringModel.Category.IsNullOrEmpty() || product.ProductType.Name.ToLower()
                .Equals(filteringModel.Category.ToLower())))
    {
        if (!string.IsNullOrEmpty(filteringModel.InStock))
        {
            Criteria = filteringModel.InStock.ToLower() switch
            {
                "true" => Criteria.And(product => product.InStock),
                "false" => Criteria.And(product => !product.InStock),
                _ => Criteria
            };
        }
        
        DeterminateSortingType(filteringModel);
        AddPaging(filteringModel.ItemQuantity, filteringModel.ItemQuantity * (filteringModel.PageIndex - 1));
    }
    
    private void DeterminateSortingType(IFilteringModel filteringModel)
    {
        switch (filteringModel.SortingType)
        {
            case "name-desc":
                AddOrderByDescending(p => p.Name);
                break;
            case "price-asc":
                AddOrderByAscending(p => p.Price);
                break;
            case "price-desc":
                AddOrderByDescending(p => p.Price);
                break;
            case "rating-desc":
                AddOrderByDescending(p => p.Rating.Score);
                break;
            default:
                AddOrderByAscending(p => p.Name);
                break;
        }
    }
}