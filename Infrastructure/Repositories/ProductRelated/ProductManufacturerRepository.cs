using Core.Entities.Product;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Common.Classes;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.FilteringModels.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.ProductRelated;

public sealed class ProductManufacturerRepository : GenericRepository<ProductManufacturer>
{
    internal ProductManufacturerRepository
        (StoreContext dbContext) : base(dbContext) => Context = dbContext;

    public async Task<Dictionary<string, int>> GetAllCountedCategoryRelatedManufacturers(
        IFilteringModel filteringModel)
    {
        var productManufacturers = await Context.ProductManufacturers.ToListAsync();

        return productManufacturers.ToDictionary(
            brand => brand.Name,
            brand => Context.Products
                .Where(product => product.ProductType.Name.Equals(filteringModel.Category.First()))
                .Count(product => product.Manufacturer.Name.Equals(brand.Name)));
    }
}