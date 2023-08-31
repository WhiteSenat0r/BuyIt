using API.Helpers.PaginationResultModels.Common.Interfaces;
using Core.Entities.Product;
using Core.Entities.Product.Common.Interfaces;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.FilteringModels.Common.Classes;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.FilteringModels.Common.Interfaces;

namespace API.Helpers.PaginationResultModels;

public class ProductPaginationResult : IPaginationResult<Product>
{
    public ProductPaginationResult
        (IEnumerable<IProduct> items, IProductFilteringModel filteringModel)
    {
        Items = items;
        ItemsQuantity = Items.Count();
        PageIndex = filteringModel.PageIndex;
        PageSize = BasicProductFilteringModel.MaximumItemQuantity;
    }
    
    public IEnumerable<IProduct> Items { get; }
    
    public int ItemsQuantity { get; }
    
    public int PageIndex { get; }

    public int PageSize { get; }
}