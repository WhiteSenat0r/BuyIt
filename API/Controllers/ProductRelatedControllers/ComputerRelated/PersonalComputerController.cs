﻿using API.Controllers.ProductRelatedControllers.Common.Classes;
using AutoMapper;
using Core.Entities.Product;
using Infrastructure.Repositories.Common.Interfaces;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.FilteringModels.ComputerRelated;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.ComputerRelatedSpecifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ProductRelatedControllers.ComputerRelated;

[ApiController]
[Route("api/[controller]s")]
public class PersonalComputerController : BaseProductController
    <PersonalComputerFilteringModel, PersonalComputerQuerySpecification>
{
    public PersonalComputerController (IRepository<Product> products, IMapper mapper)
        : base(products, mapper) { }
}