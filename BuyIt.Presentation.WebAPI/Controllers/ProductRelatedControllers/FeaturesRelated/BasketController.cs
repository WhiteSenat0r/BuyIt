using Application.DataTransferObjects.ProductListRelated.ListItems;
using AutoMapper;
using BuyIt.Presentation.WebAPI.Controllers.ProductRelatedControllers.FeaturesRelated.Common;
using Domain.Contracts.RepositoryRelated.NonRelational;
using Domain.Entities.ProductListRelated;
using Microsoft.AspNetCore.Mvc;

namespace BuyIt.Presentation.WebAPI.Controllers.ProductRelatedControllers.FeaturesRelated;

[ApiController]
[Route("api/[controller]")]
public class BasketController : BaseFeatureController<BasketItem, BasketItemDto>
{
    public BasketController(
        INonRelationalRepository<ProductList<BasketItem>> repository, IMapper mapper)
        : base(repository, mapper) { }
}