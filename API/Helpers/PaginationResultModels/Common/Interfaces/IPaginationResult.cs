using API.Helpers.DataTransferObjects.ProductRelated.Common.Interfaces;

namespace API.Helpers.PaginationResultModels.Common.Interfaces;

public interface IPaginationResult
{
    IEnumerable<IProductDto> Items { get; }
    
    public int TotalItemsQuantity { get; }
    
    int CurrentPageItemsQuantity { get; }
    
    int PageIndex { get; }
}