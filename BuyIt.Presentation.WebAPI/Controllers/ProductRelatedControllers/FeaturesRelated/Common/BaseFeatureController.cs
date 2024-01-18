using Application.DataTransferObjects.ProductListRelated;
using AutoMapper;
using BuyIt.Presentation.WebAPI.Controllers.Common.Classes;
using Domain.Contracts.ProductListRelated;
using Domain.Contracts.RepositoryRelated.NonRelational;
using Domain.Entities.ProductListRelated;
using Microsoft.AspNetCore.Mvc;

namespace BuyIt.Presentation.WebAPI.Controllers.ProductRelatedControllers.FeaturesRelated.Common
{
    public abstract class BaseFeatureController<TProductListItem, TProductListItemDto> : BaseApiController
        where TProductListItem : class, IProductListItem, new()
        where TProductListItemDto : class, IProductListItem, new()
    {
        private readonly INonRelationalRepository<ProductList<TProductListItem>> _repository;
        private readonly IMapper _mapper;

        protected BaseFeatureController(
            INonRelationalRepository<ProductList<TProductListItem>> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
            
            var updatedList = User.Identity!.IsAuthenticated 
                ? await _repository.CreateOrUpdateEntityAsync(list)
                : await _repository.CreateOrUpdateEntityAsync(list, 7);

            return Ok(updatedList);
        }
        
        [HttpDelete("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete(Guid listId)
        {
            var removalResult = await _repository.RemoveExistingEntityAsync(listId);
            
            return removalResult 
                ? Ok("List was successfully removed!")
                : BadRequest("List was not removed!");
        }
    }
}
