using System.Linq.Expressions;
using Core.Entities.Product;
using Infrastructure.Repositories.Common.QuerySpecifications.Common.Classes;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common
    .FilteringModels.Common.Interfaces;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.Classes;

public abstract class BasicProductQuerySpecification : QuerySpecification<Product>
{
    protected BasicProductQuerySpecification(IProductFilteringModel filteringModel) 
        : base(product => 
            (!filteringModel.LowerPriceLimit.HasValue || product.Price >= filteringModel.LowerPriceLimit) &&
            (!filteringModel.UpperPriceLimit.HasValue || product.Price <= filteringModel.UpperPriceLimit) &&
            (string.IsNullOrEmpty(filteringModel.BrandName) || product.Manufacturer.Name.ToLower().Equals
                (filteringModel.BrandName.ToLower())))
    {
        AddIncludeRange(new List<Expression<Func<Product, object>>>
        {
            p => p.Manufacturer,
            p => p.ProductType,
            p => p.Rating,
        });

        DeterminateSortingType(filteringModel);
    }

    private void DeterminateSortingType(IProductFilteringModel filteringModel)
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
    
    public Expression<Func<Product, bool>> SpecificationCriteria { get; protected init; }
    
    protected static bool FilterBySpecificationAspect(string incomeAspect, string parentKey, 
        string childKey, IDictionary<string, IDictionary<string,string>> specifications,
        Func<IDictionary<string, IDictionary<string,string>>, string, string, string, bool> getCriteria) =>
        string.IsNullOrEmpty(incomeAspect) || getCriteria(specifications, incomeAspect, parentKey, childKey);

    private static string GetConvertedInputSpecificationAspectValue(string inputValue, char containedChar) =>
        inputValue.Contains(containedChar) ? inputValue.Replace(containedChar, ' ') : inputValue;
    
    protected static bool GetRequiredCriteria(IDictionary<string, IDictionary<string,string>> specifications,
        string classification, string parentKey, string childKey) =>
        specifications.ContainsKey(parentKey) &&
        specifications[parentKey].ContainsKey(childKey) &&
        specifications[parentKey][childKey].ToLower().Equals(
            GetConvertedInputSpecificationAspectValue(classification, '_').ToLower());
}