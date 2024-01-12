using AutoMapper;
using BuyIt.Presentation.WebAPI.Controllers.Common.Classes;
using Domain.Contracts.RepositoryRelated.Relational;
using Domain.Entities.ProductRelated;

namespace BuyIt.Presentation.WebAPI.Controllers.Common;

public class ProductManufacturerController : BaseApiController
{
    private readonly IRepository<ProductManufacturer> _manufacturers;

    public ProductManufacturerController(IRepository<ProductManufacturer> manufacturers, IMapper mapper)
    {
        _manufacturers = manufacturers;
        Mapper = mapper;
    }
    
    private IMapper Mapper { get; }
}