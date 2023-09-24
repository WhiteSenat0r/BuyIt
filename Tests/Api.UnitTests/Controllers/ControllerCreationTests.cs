using API.Controllers.Common;
using API.Controllers.ProductRelatedControllers.Common.Classes;
using API.Controllers.ProductRelatedControllers.ComputerRelated;
using AutoMapper;
using Core.Entities.Product;
using Infrastructure.Repositories.Common.Interfaces;
using Moq;

namespace Tests.Api.UnitTests.Controllers;

public class ControllerCreationTests
{
    private readonly Mock<IMapper> _mapper = new();

    [Theory]
    [InlineData(typeof(PersonalComputerController))]
    [InlineData(typeof(LaptopController))]
    [InlineData(typeof(AllInOneComputerController))]
    [InlineData(typeof(ProductSearchController))]
    public void Controller_Constructor_Should_CreateInstance(Type controllerType)
    {
        var repository = new Mock<IRepository<Product>>();
        var controller = Activator.CreateInstance(controllerType, repository.Object, _mapper.Object);

        Assert.NotNull(controller);
    }
    
    [Fact]
    public void ProductManufacturerController_Constructor_Should_CreateInstance()
    {
        var repository = new Mock<IRepository<ProductManufacturer>>();
        
        var controller = new ProductManufacturerController(repository.Object, _mapper.Object);

        Assert.NotNull(controller);
    }
}