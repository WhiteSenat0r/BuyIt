using Application.DataTransferObjects.ProductListRelated.ListItems;
using AutoMapper;
using BuyIt.Presentation.WebAPI.Controllers.ProductRelatedControllers.FeaturesRelated.Common;
using Domain.Contracts.RepositoryRelated.NonRelational;
using Domain.Entities.IdentityRelated;
using Domain.Entities.ProductListRelated;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BuyIt.Presentation.WebAPI.Controllers.ProductRelatedControllers.FeaturesRelated;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class WishlistController : BaseFeatureController<WishedItem, WishedItemDto>
{
    public WishlistController(
        INonRelationalRepository<ProductList<WishedItem>> repository,
        UserManager<User> userManager,
        IMapper mapper)
        : base(repository, mapper, userManager) { }
}