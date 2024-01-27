using Application.Contracts;
using Application.DataTransferObjects.ProductRelated.Specification;
using Application.FilteringModels;
using Application.Helpers.SpecificationResolver.Common;
using Application.Helpers.SpecificationResolver.Common.Constants;
using Application.Specifications.ProductManufacturerSpecifications;
using Application.Specifications.ProductSpecifications;
using Application.Specifications.ProductTypeSpecifications;
using Domain.Contracts.RepositoryRelated.Relational;
using Domain.Entities.ProductRelated;
using Microsoft.IdentityModel.Tokens;

namespace Application.Helpers.SpecificationResolver;

public sealed class ProductSpecificationFilterResolver
{
    public async Task<FilterDto> ResolveAsync(IRepository<Product> products,
        IRepository<ProductSpecification> productSpecs,
        IRepository<ProductManufacturer> manufacturers,
        IRepository<ProductType> categories,
        IFilteringModel filteringModel)
    {
        var productManufacturers = await manufacturers.GetAllEntitiesAsync(
            new ProductManufacturerQuerySpecification(true));

        var specifications = await GetAllSpecifications(productSpecs, filteringModel);
        
        var filteredProducts = await products.GetAllEntitiesAsync(
            GetQuerySpecification(filteringModel));
        
        var storedBrands = filteringModel.BrandName;
        
        var brandlessSpecification = GetBrandlessQuerySpecification(filteringModel);

        var brandLessFilteredProducts = await products.GetAllEntitiesAsync(brandlessSpecification);

        filteringModel.BrandName = storedBrands;

        var categoryRelatedProducts = await GetCategoryRelatedProducts(
            products, filteringModel, filteredProducts);

        var categoryConstants = new FilterCategoryConstants();
        var attributeConstants = new FilterAttributeConstants();
        
        var specificationExtractor = InitializeProductSpecificationExtractor(
            filteringModel, categoryRelatedProducts, filteredProducts,
            specifications, productManufacturers, categoryConstants, attributeConstants);

        var commonSpecifications = specificationExtractor.ExtractCommonSpecifications();

        var productCategories = await GetAllProductCategoriesAsync(
            filteringModel, categories, filteredProducts);
        
        var filterCounter = InitializeFilterCounter(
            filteringModel, categoryRelatedProducts, filteredProducts,
            brandLessFilteredProducts, commonSpecifications, productManufacturers,
            productCategories, specificationExtractor);
        
        var countedBrands = filterCounter.GetCountedBrands();
        
        var countedSpecs = filterCounter.GetCountedSpecifications();
        
        var countedCategories = filterCounter.GetCountedCategories();

        return new FilterDto
        {
            CountedBrands = countedBrands.OrderBy(pair => pair.Key).ToDictionary(
                pair => pair.Key, pair => pair.Value),
            CountedSpecifications = countedSpecs,
            CountedCategories = countedCategories,
            MinPrice = filteredProducts.Count > 0 ? (int)filteredProducts.MinBy(
                product => Math.Round(product.Price)).Price : 0,
            MaxPrice = filteredProducts.Count > 0 ? Convert.ToInt32(filteredProducts.MaxBy(
                product => Math.Round(product.Price)).Price) : 0
        };
    }

    private static async Task<List<ProductType>> GetAllProductCategoriesAsync(
        IFilteringModel filteringModel, 
        IRepository<ProductType> categories,
        IEnumerable<Product> filteredProducts)
    {
        var filterCategories = filteringModel.Category;

        filteringModel.Category = new List<string>();
        
        var extractedCategories = filteringModel.BrandName.IsNullOrEmpty()
            ? await categories.GetAllEntitiesAsync(
                new ProductTypeQuerySpecification(c => c.Products.Count > 0))
            : await categories.GetAllEntitiesAsync(
                new ProductTypeQueryByManufacturerSpecification(
                    filteredProducts.Select(product => product.Manufacturer.Name)));

        filteringModel.Category = filterCategories;
        
        return extractedCategories;
    }

    private ProductFilterCounter InitializeFilterCounter(
        IFilteringModel filteringModel, IEnumerable<Product> categoryRelatedProducts,
        IEnumerable<Product> filteredProducts, IEnumerable<Product> brandLessFilteredProducts,
        IEnumerable<ProductSpecification> commonSpecifications,
        IEnumerable<ProductManufacturer> productManufacturers,
        IEnumerable<ProductType> productCategories,
        ProductSpecificationExtractor specificationExtractor) => new(
            categoryRelatedProducts, filteredProducts, brandLessFilteredProducts,
            commonSpecifications, productManufacturers, productCategories,
            filteringModel, specificationExtractor);

    private ProductSpecificationExtractor InitializeProductSpecificationExtractor(
        IFilteringModel filteringModel, IEnumerable<Product> categoryRelatedProducts,
        IEnumerable<Product> filteredProducts, IEnumerable<ProductSpecification> specifications,
        IEnumerable<ProductManufacturer> productManufacturers, FilterCategoryConstants categoryConstants,
        FilterAttributeConstants attributeConstants) => new(
            categoryRelatedProducts, filteredProducts, specifications,
            productManufacturers, filteringModel,
            categoryConstants, attributeConstants);

    private static async Task<List<ProductSpecification>> GetAllSpecifications(
        IRepository<ProductSpecification> productSpecs, IFilteringModel filteringModel) =>
        filteringModel.GetType() != typeof(ProductSearchFilteringModel) 
            ? await productSpecs.GetAllEntitiesAsync(
                new ProductSpecificationQuerySpecification(
                    s => s.Products.Any(
                        p => p.ProductType.Name.Equals(filteringModel.Category.First())))) 
            : new List<ProductSpecification>();

    private static IQuerySpecification<Product> GetBrandlessQuerySpecification(IFilteringModel filteringModel)
    {
        filteringModel.BrandName = new List<string>();

        filteringModel.CreateQuerySpecification();

        var brandlessSpecification = filteringModel.CreateQuerySpecification();

        brandlessSpecification.IsPagingEnabled = false;
        brandlessSpecification.IsNotTracked = true;
        
        return brandlessSpecification;
    }

    private static async Task<IEnumerable<Product>> GetCategoryRelatedProducts(
        IRepository<Product> products, IFilteringModel filteringModel,
        IReadOnlyCollection<Product> filteredProducts)
    {
        return filteringModel is ProductSearchFilteringModel 
               || filteredProducts.Count == 0 
            ? new List<Product>() 
            : (await products.GetAllEntitiesAsync(new ProductQuerySpecification(
                product => product.ProductType.Name.Equals(
                    filteredProducts.First().ProductType.Name), true))).Except(filteredProducts);
    }
    
    private IQuerySpecification<Product> GetQuerySpecification(IFilteringModel filteringModel)
    {
        var result = filteringModel.CreateQuerySpecification();
        
        result.IsPagingEnabled = false;

        return result;
    }
}