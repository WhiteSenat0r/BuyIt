namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.FilteringModels.Common.Interfaces;

public interface IFilteringModel
{
    string Category { get; }
    
    string BrandName { get; }

    decimal? LowerPriceLimit { get; }
    
    decimal? UpperPriceLimit { get; }
    
    string InStock { get; } // true - in stock, false - not in stock
    
    string SortingType { get; }
    
    public int PageIndex { get; set; }

    public int ItemQuantity { get; set; }
}