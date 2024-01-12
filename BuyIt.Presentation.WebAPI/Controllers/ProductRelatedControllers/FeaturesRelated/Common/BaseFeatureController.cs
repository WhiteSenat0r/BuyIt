using BuyIt.Presentation.WebAPI.Controllers.Common.Classes;
using Domain.Contracts.ProductListRelated;
using Domain.Contracts.RepositoryRelated.NonRelational;
using Domain.Entities.ProductListRelated;
using Microsoft.AspNetCore.Mvc;

namespace BuyIt.Presentation.WebAPI.Controllers.ProductRelatedControllers.FeaturesRelated.Common
{
    public abstract class BaseFeatureController<TProductListItem> : BaseApiController
        where TProductListItem : class, IProductListItem, new()
    {
        private readonly INonRelationalRepository<ProductList<TProductListItem>> _repository;

        protected BaseFeatureController(
            INonRelationalRepository<ProductList<TProductListItem>> repository) =>
            _repository = repository;

        [HttpGet]
        [ProducesResponseType(typeof(IProductList<IProductListItem>), StatusCodes.Status200OK)]
        public virtual async Task<ActionResult<IProductList<TProductListItem>>> Get(Guid listId) => 
            Ok(await _repository.GetSingleEntityByIdAsync(listId));

        [HttpPut("update")]
        [ProducesResponseType(typeof(IProductList<IProductListItem>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IProductList<TProductListItem>>> Update([FromBody]
            ProductList<TProductListItem> list)
        {
            var updatedList = User.Identity is not null 
                ? await _repository.UpdateExistingEntityAsync(list)
                : await _repository.UpdateExistingEntityAsync(list, 7);

            return Ok(updatedList);
        }
        
        [HttpDelete("delete")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete(Guid listId)
        {
            var removalResult = await _repository.RemoveExistingEntityAsync(listId);
            
            return removalResult 
                ? Ok("List was successfully removed!")
                : BadRequest("List was not removed!");
        }
    }
}
