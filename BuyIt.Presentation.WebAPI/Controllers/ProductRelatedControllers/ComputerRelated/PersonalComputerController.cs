using Application.FilteringModels;
using Application.Specifications.ProductSpecifications.ComputerRelatedSpecifications;
using AutoMapper;
using BuyIt.Presentation.WebAPI.Controllers.ProductRelatedControllers.Common.Classes;
using Domain.Contracts.RepositoryRelated.Relational;
using Domain.Entities.ProductRelated;
using Microsoft.AspNetCore.Mvc;

namespace BuyIt.Presentation.WebAPI.Controllers.ProductRelatedControllers.ComputerRelated;

[ApiController]
[Route("api/[controller]")]
public class PersonalComputerController : BaseProductController
    <PersonalComputerFilteringModel, PersonalComputerQuerySpecification>
{
    public PersonalComputerController (IRepository<Product> products, IMapper mapper)
        : base(products, mapper) { }
}