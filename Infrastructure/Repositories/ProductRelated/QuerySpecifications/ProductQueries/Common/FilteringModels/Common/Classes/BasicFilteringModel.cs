using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common
    .FilteringModels.Common.Interfaces;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common
    .FilteringModels.Common.Classes;

public abstract class BasicFilteringModel : IFilteringModel
{
    private int _itemQuantity = MaximumItemQuantity;

    public const int MaximumItemQuantity = 24;

    public int PageIndex { get; set; } = 1;

    public int ItemQuantity
    {
        get => _itemQuantity;
        set => _itemQuantity = value > MaximumItemQuantity ? MaximumItemQuantity : value;
    }

    public List<string> Category { get; set; } = new();

    public List<string> BrandName { get; } = new();

    public decimal? LowerPriceLimit { get; set; }
    
    public decimal? UpperPriceLimit { get; set; }
    
    public string InStock { get; set; }

    public string SortingType { get; set; }
}