using Application.DataTransferObjects.ProductRelated.Specification;
using Application.FilteringModels;
using Application.Helpers.SpecificationResolver;
using BuyIt.Presentation.WebAPI.Controllers.Common.Classes;
using Domain.Contracts.RepositoryRelated;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BuyIt.Presentation.WebAPI.Controllers.Common;

public class SpecificationFilterController : BaseApiController
{
    private readonly IRepository<ProductManufacturer> _manufacturers;
    private readonly IRepository<ProductSpecification> _productSpecifications;
    private readonly IRepository<ProductType> _categories;
    private readonly IRepository<Product> _products;
    private readonly ProductSpecificationFilterResolver _filterResolver;

    public SpecificationFilterController(IRepository<ProductManufacturer> manufacturers,
        IRepository<Product> products, IRepository<ProductType> categories, 
        ProductSpecificationFilterResolver filterResolver, 
        IRepository<ProductSpecification> productSpecifications)
    {
        _products = products;
        _filterResolver = filterResolver;
        _productSpecifications = productSpecifications;
        _categories = categories;
        _manufacturers = manufacturers;
    }

    [HttpGet("personalcomputer")]
    public async Task<ActionResult<FilterDto>> GetAll(
        [FromQuery] PersonalComputerFilteringModel filteringModel) =>
        Ok(await _filterResolver.ResolveAsync(
            _products, _productSpecifications, _manufacturers, _categories, filteringModel));

    [HttpGet("productsearch")]
    public async Task<ActionResult<FilterDto>> GetAll(
        [FromQuery] ProductSearchFilteringModel filteringModel) =>
        Ok(await _filterResolver.ResolveAsync(
            _products, _productSpecifications, _manufacturers, _categories, filteringModel));
}