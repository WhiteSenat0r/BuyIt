using API.Controllers.Common.Classes;
using API.Helpers.DataTransferObjects.ProductRelated.Specification;
using API.Helpers.Resolvers;
using Core.Entities.Product;
using Infrastructure.Repositories.Common.Interfaces;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.FilteringModels;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.FilteringModels.ComputerRelated;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Common;

public class SpecificationFilterController : BaseApiController
{
    private readonly IRepository<ProductManufacturer> _manufacturers;
    private readonly IRepository<ProductType> _categories;
    private readonly IRepository<Product> _products;
    private readonly ProductSpecificationFilterResolver _filterResolver;

    public SpecificationFilterController(IRepository<ProductManufacturer> manufacturers,
        IRepository<Product> products, IRepository<ProductType> categories, 
        ProductSpecificationFilterResolver filterResolver)
    {
        _products = products;
        _filterResolver = filterResolver;
        _categories = categories;
        _manufacturers = manufacturers;
    }

    [HttpGet("personalcomputer")]
    public async Task<ActionResult<FilterDto>> GetAll(
        [FromQuery] PersonalComputerFilteringModel filteringModel) =>
        Ok(await _filterResolver.ResolveAsync(
            _products, _manufacturers, _categories, filteringModel));

    [HttpGet("search")]
    public async Task<ActionResult<FilterDto>> GetAll(
        [FromQuery] ProductSearchFilteringModel filteringModel) =>
        Ok(await _filterResolver.ResolveAsync(
            _products, _manufacturers, _categories, filteringModel));
}