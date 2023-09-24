using API.Controllers.Common;
using API.Helpers.DataTransferObjects.Manufacturer;
using AutoMapper;
using Core.Entities.Product;
using Infrastructure.Repositories.Common.Interfaces;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductManufacturerQueries;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductManufacturerQueries.Common.FilteringModels;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Tests.Api.UnitTests.Controllers;

public class ProductManufacturerControllerTests
{
    private readonly ProductManufacturerController _controller;
    private readonly Mock<IRepository<ProductManufacturer>> _mockManufacturersRepository;
    private readonly Mock<IMapper> _mockMapper;

    public ProductManufacturerControllerTests()
    {
        _mockManufacturersRepository = new Mock<IRepository<ProductManufacturer>>();
        _mockMapper = new Mock<IMapper>();
        _controller = new ProductManufacturerController(_mockManufacturersRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResultWithProductManufacturers()
    {
        // Arrange
        var filteringModel = new ProductManufacturerFilteringModel { ProductCategory = "Test" };
        var productManufacturers = new List<ProductManufacturer>(); 
        var productManufacturerDtos = new List<ProductManufacturerDto>();

        _mockManufacturersRepository.Setup(repo => repo.GetAllEntitiesAsync(
                It.IsAny<ProductManufacturerByProductTypeQuerySpecification>()))
            .ReturnsAsync(productManufacturers);

        _mockMapper.Setup(mapper => mapper.Map<IEnumerable<ProductManufacturerDto>>(productManufacturers))
            .Returns(productManufacturerDtos);

        var result = await _controller.GetAll(filteringModel);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedDtos = Assert.IsAssignableFrom<IEnumerable<ProductManufacturerDto>>(okResult.Value);
    }
}