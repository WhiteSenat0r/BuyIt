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
                    if (await IsUpdatableListWithExistingId(
                            user.BasketId, listId) is false)
                        return GetFailedSynchronizationResult();
                    user.BasketId = listId;
                    break;
                case "WISHLIST":
                    if (await IsUpdatableListWithExistingId(
                            user.WishListId, listId) is false)
                        return GetFailedSynchronizationResult();
                    user.WishListId = listId;
                    break;
                case "COMPARISONLIST":
                    if (await IsUpdatableListWithExistingId(
                            user.ComparisonListId, listId) is false)
                        return GetFailedSynchronizationResult();
                    user.ComparisonListId = listId;
                    break;
            }
            
            var result = await _userManager.UpdateAsync(user);

            return !result.Succeeded ? GetFailedSynchronizationResult() : Ok();
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

        private async Task<bool?> IsUpdatableListWithExistingId(
            Guid? currentListId, Guid? synchronizedListId)
        {
            if (currentListId is null) return null;

            var synchronizedList = await _repository.GetSingleEntityByIdAsync(synchronizedListId!.Value);

            if (!synchronizedList.Items.Any()) return true;
            
            var currentList = await _repository.GetSingleEntityByIdAsync(currentListId.Value);
            
            await JoinListsAsync(currentList, synchronizedList);
            
            var removalResult = await _repository.RemoveExistingEntityAsync(currentListId.Value);

            return removalResult;
        }

        private async Task JoinListsAsync(ProductList<TProductListItem> currentList,
            ProductList<TProductListItem> synchronizedList)
        {
            foreach (var item in currentList.Items)
            {
                if (synchronizedList.Items.SingleOrDefault(
                            i => i.ProductCode.Equals(item.ProductCode)) 
                        is not null
                    && item is BasketItem)
                {
                    var updatedItem = synchronizedList.Items.Single(
                        i => i.ProductCode.Equals(item.ProductCode)) as BasketItem;

                    updatedItem!.Quantity++;

                    var removedIndex = (synchronizedList.Items as List<TProductListItem>)!.IndexOf(
                        (synchronizedList.Items as List<TProductListItem>)!.First(i =>
                            i.ProductCode.Equals(updatedItem.ProductCode)));
                    
                    (synchronizedList.Items as List<TProductListItem>)!.RemoveAt(removedIndex);
                    (synchronizedList.Items as List<TProductListItem>)!.Add(updatedItem as TProductListItem);
                    
                    continue;
                }
                
                if (synchronizedList.Items.SingleOrDefault(
                        i => i.ProductCode.Equals(item.ProductCode)) is null)
                    (synchronizedList.Items as List<TProductListItem>)!.Add(item);
            }
            
            await _repository.CreateOrUpdateEntityAsync(synchronizedList);
        }

        private ActionResult GetFailedSynchronizationResult()
        {
            return BadRequest(new ApiResponse(
                400, "Synchronization was not performed due to an error!"));
        }
    }
}
