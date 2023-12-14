using Application.Contracts;
using Application.DataTransferObjects.ProductRelated;
using Application.Helpers;
using Application.Responses.Common.Classes;
using Application.Specifications.ProductSpecifications;
using AutoMapper;
using BuyIt.Presentation.WebAPI.Controllers.Common.Classes;
using Domain.Contracts.RepositoryRelated;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BuyIt.Presentation.WebAPI.Controllers.ProductRelatedControllers.Common.Classes;

public abstract class BaseProductRelatedController<TFilteringModel, TQuerySpecification> : BaseApiController
    where TFilteringModel : IFilteringModel
    where TQuerySpecification : BasicProductFilteringQuerySpecification
{
    private protected readonly IRepository<Product> Products;
    
    protected BaseProductRelatedController(IRepository<Product> products, IMapper mapper)
    {
        Products = products;
        Mapper = mapper;
    }
    
    private protected IMapper Mapper { get; }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IPaginationResult>> GetAll([FromQuery] TFilteringModel filteringModel)
    {
        var receivedData = await Products.GetAllEntitiesAsync(
            (TQuerySpecification)Activator.CreateInstance(typeof(TQuerySpecification), filteringModel));
        
        return receivedData.Count == 0 
            ? NotFound(new ApiResponse(404, "No items were found!"))
            : Ok(new ProductPaginationResult(
                Mapper.Map<List<GeneralizedProductDto>>(receivedData),
                filteringModel, 
                await Products.CountAsync((TQuerySpecification)Activator.CreateInstance(
                    typeof(TQuerySpecification), filteringModel))));
    }
}