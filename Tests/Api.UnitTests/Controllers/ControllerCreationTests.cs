using API.Controllers.ProductRelatedControllers.Common.Classes;
using API.Controllers.ProductRelatedControllers.ComputerRelated;
using AutoMapper;
using Core.Entities.Product;
using Infrastructure.Repositories.Common.Interfaces;
using Moq;

namespace Tests.Api.UnitTests.Controllers;

public class ControllerCreationTests
{
    private readonly Mock<IMapper> _mapper ;
    private readonly Mock<IRepository<Product>> _repository;

    public ControllerCreationTests()
    {
        _mapper = new Mock<IMapper>();
        _repository = new Mock<IRepository<Product>>();
    }
    
    [Theory]
    [InlineData(typeof(PersonalComputersController))]
    [InlineData(typeof(LaptopsController))]
    [InlineData(typeof(AllInOneComputersController))]
    [InlineData(typeof(ProductSearchController))]
    public void Controller_Constructor_Should_CreateInstance(Type controllerType)
    {
        var controller = Activator.CreateInstance(controllerType, _repository.Object, _mapper.Object);

        Assert.NotNull(controller);
    }
}