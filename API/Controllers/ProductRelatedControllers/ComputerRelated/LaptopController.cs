using API.Controllers.ProductRelatedControllers.Common.Classes;
using AutoMapper;
using Core.Entities.Product;
using Infrastructure.Repositories.Common.Interfaces;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.FilteringModels.ComputerRelated;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.ComputerRelatedSpecifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ProductRelatedControllers.ComputerRelated;

[ApiController]
[Route("api/[controller]")]
public sealed class LaptopController : BaseProductController<LaptopFilteringModel, LaptopQuerySpecification>
{
    public LaptopController(IRepository<Product> products, IMapper mapper) 
        : base(products, mapper) { }
}