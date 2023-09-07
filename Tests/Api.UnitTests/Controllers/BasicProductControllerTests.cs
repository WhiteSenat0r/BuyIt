using API.Controllers.ProductRelatedControllers.ComputerRelated;
using AutoMapper;
using Core.Entities.Product;
using Infrastructure.Repositories.Common.Interfaces;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.FilteringModels.ComputerRelated;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.ComputerRelatedSpecifications;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.RegularSpecifications;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Tests.Api.UnitTests.Controllers;

public class BasicProductControllerTests
{
    [Fact]
    public async Task GetAll_Should_Return_NotFound_IfNoProductsArePresent()
    {
        var filteringModel = new AioComputerFilteringModel { PageIndex = 1 };
        var mockMapper = new Mock<IMapper>();
        var mockProductRepository = new Mock<IRepository<Product>>();

        mockProductRepository.Setup(c => c.GetAllEntitiesAsync(
                It.IsAny<AioComputerQuerySpecification>()))
            .ReturnsAsync(new List<Product>());

        var controller = new AllInOneComputersController(mockProductRepository.Object, mockMapper.Object);

        var result = await controller.GetAll(filteringModel);

        Assert.IsType<NotFoundObjectResult>(result.Result);
    }
    
    [Fact]
    public async Task Get_Should_Return_NotFound_IfProductIsNotFound()
    {
        var productCode = "NonExistentProductCode";
        var mockMapper = new Mock<IMapper>();
        var mockProductRepository = new Mock<IRepository<Product>>();

        mockProductRepository.Setup(c => c.GetSingleEntityBySpecificationAsync(
            It.IsAny<ProductQueryByProductCodeSpecification>()))
            .ReturnsAsync((Product)null);

        var controller = new AllInOneComputersController(mockProductRepository.Object, mockMapper.Object);

        var result = await controller.Get(productCode);

        Assert.IsType<NotFoundObjectResult>(result.Result);
    }
}