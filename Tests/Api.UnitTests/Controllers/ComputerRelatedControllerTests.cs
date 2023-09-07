using API.Controllers.ProductRelatedControllers.ComputerRelated;
using AutoMapper;
using Core.Entities.Product;
using Infrastructure.Repositories.Common.Interfaces;
using Moq;

namespace Tests.Api.UnitTests.Controllers;

public class ComputerRelatedControllerTests
{
    private Mock<IMapper> _mapper ;
    private Mock<IRepository<Product>> _repository;

    [Fact]
    public void PersonalComputersController_Constructor_Should_CreateInstance()
    {
        _mapper = new Mock<IMapper>();
        _repository = new Mock<IRepository<Product>>();

        var controller = new PersonalComputersController(_repository.Object, _mapper.Object);

        Assert.NotNull(controller);
    }
    
    [Fact]
    public void LaptopsController_Constructor_Should_CreateInstance()
    {
        _mapper = new Mock<IMapper>();
        _repository = new Mock<IRepository<Product>>();

        var controller = new LaptopsController(_repository.Object, _mapper.Object);

        Assert.NotNull(controller);
    }
    
    [Fact]
    public void AllInOneComputersController_Constructor_Should_CreateInstance()
    {
        _mapper = new Mock<IMapper>();
        _repository = new Mock<IRepository<Product>>();

        var controller = new AllInOneComputersController(_repository.Object, _mapper.Object);

        Assert.NotNull(controller);
    }
}