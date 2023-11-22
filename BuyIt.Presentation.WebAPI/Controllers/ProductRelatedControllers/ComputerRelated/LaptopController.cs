using Application.FilteringModels;
using Application.Specifications.ProductSpecifications.ComputerRelatedSpecifications;
using AutoMapper;
using BuyIt.Presentation.WebAPI.Controllers.ProductRelatedControllers.Common.Classes;
using Domain.Contracts.RepositoryRelated;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BuyIt.Presentation.WebAPI.Controllers.ProductRelatedControllers.ComputerRelated;

[ApiController]
[Route("api/[controller]")]
public sealed class LaptopController : BaseProductController<LaptopFilteringModel, LaptopQuerySpecification>
{
    public LaptopController(IRepository<Product> products, IMapper mapper) 
        : base(products, mapper) { }
}