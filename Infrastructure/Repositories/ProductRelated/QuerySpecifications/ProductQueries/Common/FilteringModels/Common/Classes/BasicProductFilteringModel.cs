using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common
    .FilteringModels.Common.Interfaces;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common
    .FilteringModels.Common.Classes;

public abstract class BasicProductFilteringModel : IProductFilteringModel
{
    private int _itemQuantity = MaximumItemQuantity;

    public const int MaximumItemQuantity = 24;

    public int PageIndex { get; set; } = 1;

    public int ItemQuantity
    {
        get => _itemQuantity;
        set => _itemQuantity = value > MaximumItemQuantity ? MaximumItemQuantity : value;
    }
    
    public string Category { get; protected init; }

    public string BrandName { get; set; }

    public decimal? LowerPriceLimit { get; set; }
    
    public decimal? UpperPriceLimit { get; set; }
    
    public string SortingType { get; set; }
}