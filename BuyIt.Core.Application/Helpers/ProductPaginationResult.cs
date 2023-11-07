using Application.Contracts;

namespace Application.Helpers;

public sealed class ProductPaginationResult : IPaginationResult
{
    public ProductPaginationResult
        (IEnumerable<IProductDto> items, IFilteringModel filteringModel, int totalItemsQuantity)
    {
        Items = items;
        CurrentPageItemsQuantity = Items.Count();
        PageIndex = filteringModel.PageIndex;
        TotalItemsQuantity = totalItemsQuantity;
    }
    
    public IEnumerable<IProductDto> Items { get; }
    
    public int TotalItemsQuantity { get; }
    
    public int CurrentPageItemsQuantity { get; }
    
    public int PageIndex { get; }
}