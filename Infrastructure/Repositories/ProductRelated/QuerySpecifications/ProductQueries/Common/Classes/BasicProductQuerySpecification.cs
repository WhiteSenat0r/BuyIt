using System.Linq.Expressions;
using Core.Entities.Product;
using Core.Entities.Product.Common.Interfaces;
using Infrastructure.Repositories.Common.QuerySpecifications.Common.Classes;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common
    .FilteringModels.Common.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.Classes;

public abstract class BasicProductQuerySpecification : QuerySpecification<Product>
{
    protected BasicProductQuerySpecification(IProductFilteringModel filteringModel) 
        : base(product => 
            (filteringModel.Category.IsNullOrEmpty() || product.ProductType.Name.ToLower().Equals
                (filteringModel.Category.ToLower())) &&
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

        AddPaging(filteringModel.ItemQuantity, filteringModel.ItemQuantity * ((filteringModel.PageIndex - 1)));
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

    protected static bool GetSpecificationCategoryFilteringModel
        (string parentKey, IEnumerable<string> childKeys, IEnumerable<string> incomeAspects, IProduct criteria)
    {
        var childKeysList = childKeys.ToList();
        var incomeAspectsList = incomeAspects.ToList();

        return !childKeysList.Where((t, i) => 
            !FilterBySpecificationAspect
                (incomeAspectsList[i], parentKey, t, criteria.Specifications, GetRequiredCriteria)).Any();
    }

    private static bool FilterBySpecificationAspect(string incomeAspect, string parentKey, 
        string childKey, IDictionary<string, IDictionary<string,string>> specifications,
        Func<IDictionary<string, IDictionary<string,string>>, string, string, string, bool> getCriteria) =>
        string.IsNullOrEmpty(incomeAspect) || getCriteria(specifications, incomeAspect, parentKey, childKey);

    private static string GetConvertedInputSpecificationAspectValue(string inputValue, char containedChar) =>
        inputValue.Contains(containedChar) ? inputValue.Replace(containedChar, ' ') : inputValue;

    private static bool GetRequiredCriteria(IDictionary<string, IDictionary<string,string>> specifications,
        string classification, string parentKey, string childKey) =>
        specifications.ContainsKey(parentKey) &&
        specifications[parentKey].ContainsKey(childKey) &&
        specifications[parentKey][childKey].ToLower().Equals(
            GetConvertedInputSpecificationAspectValue(classification, '_').ToLower());
}