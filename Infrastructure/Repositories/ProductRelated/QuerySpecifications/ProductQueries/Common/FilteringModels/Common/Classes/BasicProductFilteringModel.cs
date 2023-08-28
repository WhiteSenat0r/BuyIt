using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common
    .FilteringModels.Common.Interfaces;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common
    .FilteringModels.Common.Classes;

public abstract class BasicProductFilteringModel : IProductFilteringModel
{
    public string Category { get; protected init; }

    public string BrandName { get; set; }

    public decimal? LowerPriceLimit { get; set; }
    
    public decimal? UpperPriceLimit { get; set; }
    
    public string SortingType { get; set; }
}