﻿using Application.FilteringModels;
using Application.Specifications.ProductSpecifications;
using AutoMapper;
using Domain.Contracts.RepositoryRelated.Relational;
using Domain.Entities.ProductRelated;
using Microsoft.AspNetCore.Mvc;

namespace BuyIt.Presentation.WebAPI.Controllers.ProductRelatedControllers.Common.Classes;

[ApiController]
public sealed class ProductSearchController : BaseProductRelatedController
    <ProductSearchFilteringModel, ProductSearchQuerySpecification>
{
    public ProductSearchController(IRepository<Product> products, IMapper mapper) : base(products, mapper) { }
}