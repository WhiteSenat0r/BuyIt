using API.Controllers.ProductRelatedControllers.Common.Classes;
using AutoMapper;
using Core.Entities.Product;
using Infrastructure.Repositories.Common.Interfaces;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.FilteringModels.ComputerRelated;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.ComputerRelatedSpecifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ProductRelatedControllers.ComputerRelated;

[ApiController]
public class AllInOneComputersController : BaseProductController
    <AioComputerFilteringModel, AioComputerQuerySpecification>
{
    public AllInOneComputersController(IRepository<Product> products, IMapper mapper) 
        : base(products, mapper) => Mapper = mapper;
}