using API.Helpers.DataTransferObjects.ProductRelated.Common.Interfaces;

namespace API.Helpers.PaginationResultModels.Common.Interfaces;

public interface IPaginationResult<TEntity> where TEntity : class
{
    IEnumerable<IProductDto> Items { get; }
    
    int ItemsQuantity { get; }
    
    int PageIndex { get; }
}