using API.Controllers.Common.Classes;
using API.Helpers.DataTransferObjects.ProductRelated;
using API.Helpers.DataTransferObjects.ProductRelated.Common.Interfaces;
using API.Helpers.PaginationResultModels;
using API.Helpers.PaginationResultModels.Common.Interfaces;
using API.Responses.Common.Classes;
using AutoMapper;
using Core.Entities.Product;
using Infrastructure.Repositories.Common.Interfaces;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.Classes;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.FilteringModels
    .Common.Interfaces;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.RegularSpecifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers.ProductRelatedControllers.Common.Classes;

public abstract class BaseProductController<TFilteringModel, TQuerySpecification> : BaseApiController
    where TFilteringModel : IProductFilteringModel
    where TQuerySpecification : BasicProductFilteringQuerySpecification
{
    protected BaseProductController(IRepository<Product> products, IMapper mapper)
    {
        Products = products;
        Mapper = mapper;
    }
    
    protected IMapper Mapper { get; init; }

    private IRepository<Product> Products { get; }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IPaginationResult<IProductDto>>> GetAll([FromQuery] TFilteringModel filteringModel)
    {
        var items = Mapper.Map<IEnumerable<GeneralizedProductDto>>(await Products.GetAllEntitiesAsync(
            (TQuerySpecification)Activator.CreateInstance(typeof(TQuerySpecification), filteringModel)));
        
        return !items.IsNullOrEmpty() ? Ok(new ProductPaginationResult(items, filteringModel)) 
            : NotFound(new ApiResponse(404, "Nothing was found!"));
    }
    
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