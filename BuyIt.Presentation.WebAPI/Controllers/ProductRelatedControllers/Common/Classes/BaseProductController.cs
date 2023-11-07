using Application.Contracts;
using Application.DataTransferObjects.ProductRelated;
using Application.FilteringModels;
using Application.Responses.Common.Classes;
using Application.Specifications;
using AutoMapper;
using Domain.Contracts.RepositoryRelated;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BuyIt.Presentation.WebAPI.Controllers.ProductRelatedControllers.Common.Classes;

public abstract class BaseProductController<TFilteringModel, TQuerySpecification> 
    : BaseProductRelatedController<TFilteringModel, TQuerySpecification>
    where TFilteringModel : BasicFilteringModel
    where TQuerySpecification : BasicProductFilteringQuerySpecification
{
    protected BaseProductController(IRepository<Product> products, IMapper mapper)
        : base(products, mapper) { }

    [HttpGet("item/{productCode}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IProductDto>> Get(string productCode)
    {
        var item = await Products.GetSingleEntityBySpecificationAsync
                (new ProductQueryByProductCodeSpecification(productCode));

        return item is null
            ? NotFound(new ApiResponse(404, 
                $"Item with given product code ({productCode}) was not found!"))
            : Ok(Mapper.Map<FullProductDto>(item));
    }
}