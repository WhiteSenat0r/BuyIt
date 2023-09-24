namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.FilteringModels.Common.Interfaces;

public interface IFilteringModel
{
    List<string> Category { get; }
    
    List<string> BrandName { get; }

    decimal? LowerPriceLimit { get; }
    
    decimal? UpperPriceLimit { get; }
    
    string InStock { get; } // true - in stock, false - not in stock
    
    string SortingType { get; }
    
    public int PageIndex { get; }

    public int ItemQuantity { get; }
}