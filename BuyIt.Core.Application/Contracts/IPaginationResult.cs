namespace Application.Contracts;

public interface IPaginationResult
{
    IEnumerable<IProductDto> Items { get; }
    
    public int TotalItemsQuantity { get; }
    
    int CurrentPageItemsQuantity { get; }
    
    int PageIndex { get; }
}