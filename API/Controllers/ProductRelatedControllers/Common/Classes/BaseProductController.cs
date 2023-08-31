using API.Controllers.Common.Classes;
using API.Helpers.DataTransferObjects.ProductRelated;
using API.Helpers.DataTransferObjects.ProductRelated.Common.Interfaces;
using API.Helpers.PaginationResultModels;
using API.Helpers.PaginationResultModels.Common.Interfaces;
using API.Responses.Common.Classes;
using AutoMapper;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Factories.ProductRelated;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.Classes;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.FilteringModels
    .Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ProductRelatedControllers.Common.Classes;

public abstract class BaseProductController<TFilteringModel, TQuerySpecification> : BaseApiController
    where TFilteringModel : IProductFilteringModel
    where TQuerySpecification : BasicProductQuerySpecification
{
    protected BaseProductController(StoreContext storeContext, IMapper mapper)
    {
        Context = storeContext;
        Mapper = mapper;
    }

    protected StoreContext Context { get; init; }
    
    protected IMapper Mapper { get; init; }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IPaginationResult<IProductDto>>> Get([FromQuery] TFilteringModel filteringModel)
    {
        return Ok(Mapper.Map<ProductPaginationResult>(new ProductPaginationResult(
            await new ProductRepositoryFactory().Create(Context).GetAllEntitiesAsync(
                (TQuerySpecification)Activator.CreateInstance(typeof(TQuerySpecification),
                    filteringModel)), filteringModel)));
    }
    
    [HttpGet("item/{productCode}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IProductDto>> GetSingle(string productCode)
    {
        var item = await new ProductRepositoryFactory().Create(Context)
            .GetSingleEntityBySpecificationAsync
                (new ProductQueryByProductCodeSpecification(productCode));

        return item is null
            ? NotFound(new ApiResponse(404, 
                $"Item with given product code ({productCode}) was not found!"))
            : Ok(Mapper.Map<ProductDto>(item));
    }
}