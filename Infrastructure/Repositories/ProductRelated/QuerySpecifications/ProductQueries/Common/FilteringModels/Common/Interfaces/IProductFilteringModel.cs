namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.FilteringModels.Common.Interfaces;

public interface IProductFilteringModel
{
    string Category { get; }
    
    string BrandName { get; }

    decimal? LowerPriceLimit { get; }
    
    decimal? UpperPriceLimit { get; }
    
    string SortingType { get; }
}