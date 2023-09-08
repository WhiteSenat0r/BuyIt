using API.Helpers.DataTransferObjects.ProductRelated.Common.Interfaces;
using API.Helpers.PaginationResultModels.Common.Interfaces;
using Core.Entities.Product;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.FilteringModels.Common.Interfaces;

namespace API.Helpers.PaginationResultModels;

public class ProductPaginationResult : IPaginationResult<Product>
{
    public ProductPaginationResult
        (IEnumerable<IProductDto> items, IFilteringModel filteringModel)
    {
        Items = items;
        ItemsQuantity = Items.Count();
        PageIndex = filteringModel.PageIndex;
    }
    
    public IEnumerable<IProductDto> Items { get; }
    
    public int ItemsQuantity { get; }
    
    public int PageIndex { get; }
}