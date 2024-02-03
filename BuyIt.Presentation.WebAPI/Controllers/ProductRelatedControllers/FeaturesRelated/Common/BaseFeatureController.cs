using System.Security.Claims;
using Application.DataTransferObjects.ProductListRelated;
using Application.Responses.Common.Classes;
using AutoMapper;
using BuyIt.Presentation.WebAPI.Controllers.Common.Classes;
using Domain.Contracts.ProductListRelated;
using Domain.Contracts.RepositoryRelated.NonRelational;
using Domain.Entities.IdentityRelated;
using Domain.Entities.ProductListRelated;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BuyIt.Presentation.WebAPI.Controllers.ProductRelatedControllers.FeaturesRelated.Common
{
    public abstract class BaseFeatureController<TProductListItem, TProductListItemDto> : BaseApiController
        where TProductListItem : class, IProductListItem, new()
        where TProductListItemDto : class, IProductListItem, new()
    {
        private readonly INonRelationalRepository<ProductList<TProductListItem>> _repository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        protected BaseFeatureController(
            INonRelationalRepository<ProductList<TProductListItem>> repository,
            IMapper mapper, UserManager<User> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IProductList<TProductListItem>>> Get(Guid listId) => 
            Ok(await _repository.GetSingleEntityByIdAsync(listId));

        [HttpPost("CreateOrUpdate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IProductList<TProductListItem>>> CreateOrUpdate([FromBody]
            ProductListDto<TProductListItemDto> dtoList)
        {
            var list = _mapper.Map<ProductListDto<TProductListItemDto>, ProductList<TProductListItem>>(dtoList);
            
            var updatedList = !Request.Cookies["UserAccessToken"].IsNullOrEmpty() 
                              || !Request.Cookies["UserRefreshToken"].IsNullOrEmpty()
                ? await _repository.CreateOrUpdateEntityAsync(list)
                : await _repository.CreateOrUpdateEntityAsync(list, 7);

            return Ok(updatedList);
        }
        
        [Authorize]
        [HttpPut("SynchronizeList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> SynchronizeListWithUser(Guid listId, string listType)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(
                u => u.Email.Equals(User.FindFirstValue(ClaimTypes.Email)));

            if (user is null) 
                return BadRequest(new ApiResponse(400, "Invalid user!"));

            switch (listType)
            {
                case "BASKET":
                    user.BasketId = listId;
                    break;
                case "WISHLIST":
                    user.WishListId = listId;
                    break;
                case "COMPARISONLIST":
                    user.ComparisonListId = listId;
                    break;
            }
            
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return BadRequest(new ApiResponse(
                        400, "Synchronization was not performed due to an error!"));
            
            return Ok(await _repository.CreateOrUpdateEntityAsync(
                await _repository.GetSingleEntityByIdAsync(listId)));
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(Guid listId)
        {
            var removalResult = await _repository.RemoveExistingEntityAsync(listId);
            
            return removalResult 
                ? Ok("List was successfully removed!")
                : BadRequest("List was not removed!");
        }
    }
}
