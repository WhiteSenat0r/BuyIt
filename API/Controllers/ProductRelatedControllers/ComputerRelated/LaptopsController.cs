using API.Controllers.ProductRelatedControllers.Common.Classes;
using AutoMapper;
using Infrastructure.Contexts;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.FilteringModels.ComputerRelated;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.ComputerRelatedSpecifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ProductRelatedControllers.ComputerRelated;

[ApiController]
public class LaptopsController : BaseProductController<LaptopFilteringModel, LaptopQuerySpecification>
{
    public LaptopsController
        (StoreContext storeContext, IMapper mapper) : base(storeContext, mapper)
    {
        Context = storeContext;
        Mapper = mapper;
    }
}