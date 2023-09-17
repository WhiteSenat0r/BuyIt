﻿using API.Controllers.Common.Classes;
using API.Helpers.DataTransferObjects.ProductRelated;
using API.Helpers.DataTransferObjects.ProductRelated.Common.Interfaces;
using API.Helpers.PaginationResultModels;
using API.Helpers.PaginationResultModels.Common.Interfaces;
using AutoMapper;
using Core.Entities.Product;
using Infrastructure.Repositories.Common.Interfaces;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.Classes;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.FilteringModels
    .Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ProductRelatedControllers.Common.Classes;

public abstract class BaseProductRelatedController<TFilteringModel, TQuerySpecification> : BaseApiController
    where TFilteringModel : IFilteringModel
    where TQuerySpecification : BasicProductFilteringQuerySpecification
{
    protected BaseProductRelatedController(IRepository<Product> products, IMapper mapper)
    {
        Products = products;
        Mapper = mapper;
    }
    
    protected IMapper Mapper { get; init; }

    protected IRepository<Product> Products { get; init; }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IPaginationResult<IProductDto>>> GetAll([FromQuery] TFilteringModel filteringModel)
    {
        var items = Mapper.Map<IEnumerable<GeneralizedProductDto>>(await Products.GetAllEntitiesAsync(
            (TQuerySpecification)Activator.CreateInstance(typeof(TQuerySpecification), filteringModel)));

        var generalizedProductDtos = items as GeneralizedProductDto[] ?? items.ToArray();

        return Ok(new ProductPaginationResult(generalizedProductDtos, filteringModel));
    }
}