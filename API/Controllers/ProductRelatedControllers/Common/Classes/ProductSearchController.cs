using AutoMapper;
using Core.Entities.Product;
using Infrastructure.Repositories.Common.Interfaces;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.Classes;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.FilteringModels;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ProductRelatedControllers.Common.Classes;

[ApiController]
public class ProductSearchController : BaseProductRelatedController
    <ProductSearchFilteringModel, ProductSearchQuerySpecification>
{
    public ProductSearchController(IRepository<Product> products, IMapper mapper) : base(products, mapper) { }
}