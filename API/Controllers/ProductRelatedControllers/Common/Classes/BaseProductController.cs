using API.Controllers.Common.Classes;
using API.Helpers.DataTransferObjects.ProductRelated;
using API.Helpers.DataTransferObjects.ProductRelated.Common.Interfaces;
using API.Responses.Common.Classes;
using AutoMapper;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Factories.ProductRelated;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ProductRelatedControllers.Common.Classes;

public class BaseProductController : BaseApiController
{
    private readonly StoreContext _context;
    private readonly IMapper _mapper;

    public BaseProductController
        (StoreContext storeContext, IMapper mapper)
    {
        _context = storeContext;
        _mapper = mapper;
    }

    protected string Category { get; init; } = null!;

    [HttpGet("[controller]/item/{productCode}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IProductDto>> GetSingleByCode(string productCode)
    {
        var item = await new ProductRepositoryFactory().Create(_context)
            .GetSingleEntityBySpecificationAsync
                (new ProductQueryByProductCodeSpecification(productCode));

        return item is null
            ? NotFound(new ApiResponse(404, 
                $"Item with given product code ({productCode}) was not found!"))
            : Ok(_mapper.Map<ProductDto>(item));
    }
}