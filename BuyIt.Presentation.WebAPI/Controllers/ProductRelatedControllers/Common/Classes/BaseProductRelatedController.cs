using Application.Contracts;
using Application.DataTransferObjects.ProductRelated;
using Application.Helpers;
using Application.Specifications;
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
    public async Task<ActionResult<IPaginationResult>> GetAll([FromQuery] TFilteringModel filteringModel)
    {
        var items = Mapper.Map<IEnumerable<GeneralizedProductDto>>(await Products.GetAllEntitiesAsync(
            (TQuerySpecification)Activator.CreateInstance(typeof(TQuerySpecification), filteringModel)));

        var generalizedProductDtos = items as GeneralizedProductDto[] ?? items.ToArray();

        return Ok(new ProductPaginationResult(generalizedProductDtos, filteringModel, await Products.CountAsync(
            (TQuerySpecification)Activator.CreateInstance(typeof(TQuerySpecification), filteringModel))));
    }
}