using Application.FilteringModels;
using Application.Specifications.ProductSpecifications;
using Application.Specifications.ProductSpecifications.ComputerRelatedSpecifications;
using AutoMapper;
using BuyIt.Presentation.WebAPI.Controllers.ProductRelatedControllers.ComputerRelated;
using Domain.Contracts.RepositoryRelated.Relational;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BuyIt.Tests.UnitTests.Api.UnitTests.Controllers;

public class BasicProductControllerTests
{
    [Fact]
    public async Task GetAll_Should_Return_NotFound_IfNoProductsArePresent()
    {
        var filteringModel = new AioComputerFilteringModel();
        var mockMapper = new Mock<IMapper>();
        var mockProductRepository = new Mock<IRepository<Product>>();

        mockProductRepository.Setup(c => c.GetAllEntitiesAsync(
                It.IsAny<AioComputerQuerySpecification>()))
            .ReturnsAsync(new List<Product>());

        var controller = new AllInOneComputerController(mockProductRepository.Object, mockMapper.Object);

        var result = await controller.GetAll(filteringModel);

        Assert.IsAssignableFrom<ActionResult>(result.Result);
    }
    
    [Fact]
    public async Task Get_Should_Return_NotFound_IfProductIsNotFound()
    {
        var productCode = "NonExistentProductCode";
        var mockMapper = new Mock<IMapper>();
        var mockProductRepository = new Mock<IRepository<Product>>();

        mockProductRepository.Setup(c => c.GetSingleEntityBySpecificationAsync(
                It.IsAny<ProductQueryByProductCodeSpecification>()))!
            .ReturnsAsync((Product)null!);

        var controller = new AllInOneComputerController(mockProductRepository.Object, mockMapper.Object);

        var result = await controller.Get(productCode);

        Assert.IsType<NotFoundObjectResult>(result.Result);
    }
}