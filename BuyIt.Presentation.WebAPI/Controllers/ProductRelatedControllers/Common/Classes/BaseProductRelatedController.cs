using System.Collections;
using Application.Contracts;
using Application.DataTransferObjects.ProductRelated;
using Application.Helpers;
using Application.Specifications.ProductSpecifications;
using AutoMapper;
using BuyIt.Presentation.WebAPI.Controllers.Common.Classes;
using Domain.Contracts.RepositoryRelated.Relational;
using Domain.Entities.ProductRelated;
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
    public async Task<ActionResult<IPaginationResult>> GetAll(
        [FromQuery] TFilteringModel filteringModel)
    {
        var receivedData = await Products.GetAllEntitiesAsync(
            (TQuerySpecification)Activator.CreateInstance(
                typeof(TQuerySpecification), filteringModel));
        
        return receivedData.Count == 0 
            ? Ok(await GetResponseResult(filteringModel, new List<Product>()))
            : Ok(await GetResponseResult(filteringModel, receivedData));
    }

    private async Task<ProductPaginationResult> GetResponseResult(
        TFilteringModel filteringModel, ICollection receivedData)
    {
        var items = receivedData.Count != 0
            ? Mapper.Map<List<GeneralizedProductDto>>(receivedData)
            : new List<GeneralizedProductDto>();
        
        return new ProductPaginationResult(items, filteringModel, 
            await Products.CountAsync((TQuerySpecification)Activator.CreateInstance(
                typeof(TQuerySpecification), filteringModel)));
    }
}