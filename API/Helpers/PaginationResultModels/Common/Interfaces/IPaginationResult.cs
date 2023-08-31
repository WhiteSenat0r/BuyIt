using Core.Entities.Product.Common.Interfaces;

namespace API.Helpers.PaginationResultModels.Common.Interfaces;

public interface IPaginationResult<TEntity> where TEntity : class
{
    IEnumerable<IProduct> Items { get; }
    
    int ItemsQuantity { get; }
    
    int PageIndex { get; }
    
    int PageSize { get; }
}