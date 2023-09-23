using API.Controllers.Common.Classes;
using API.Helpers.DataTransferObjects.Manufacturer;
using AutoMapper;
using Core.Entities.Product;
using Infrastructure.Repositories.Common.Interfaces;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductManufacturerQueries;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductManufacturerQueries.Common.FilteringModels;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Common;

public class ProductManufacturerController : BaseApiController
{
    private readonly IRepository<ProductManufacturer> _manufacturers;

    public ProductManufacturerController(IRepository<ProductManufacturer> manufacturers, IMapper mapper)
    {
        _manufacturers = manufacturers;
        Mapper = mapper;
    }
    
    private IMapper Mapper { get; }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductManufacturerDto>>> GetAll(
        ProductManufacturerFilteringModel filteringModel)
    {
        var brands = Mapper.Map<IEnumerable<ProductManufacturerDto>>(await _manufacturers.GetAllEntitiesAsync(
            new ProductManufacturerByProductTypeQuerySpecification(filteringModel)));
        
        return Ok(brands);
    }
}