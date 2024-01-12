using Application.Specifications.ProductTypeSpecifications;
using Domain.Contracts.RepositoryRelated.Relational;
using Domain.Entities.ProductRelated;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Repositories.Factories.RelationalRepositoryFactories.ProductRelated;
using Xunit;

namespace BuyIt.Tests.UnitTests.Infrastructure.UnitTests.RepositoryRelatedTests.ProductRelatedRepositoryTests;

public class GenericRepositoryTests // Tests of default repository implementation without any overrides
                                    // (in this case was used ProductTypeRepository, also represents
                                    // the testing logic for ProductManufacturerRepository and
                                    // ProductRatingRepository)
{
    private IRepository<ProductType> _repository = null!;
    private StoreContext _context = null!;
    
    [Fact]
    public async void UpdateExistingEntityMethod_Should_UpdateEntityInDatabase()
    {
        _context = new StoreContext(new DbContextOptionsBuilder<StoreContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);

        _repository = new ProductTypeRepositoryFactory().Create(_context);

        const string initialName = "Name";
        
        var type = new ProductType(initialName);

        await _repository.AddNewEntityAsync(type);
        
        Assert.Equal(type, await _repository.GetSingleEntityBySpecificationAsync
            (new ProductTypeQueryByIdSpecification(type.Id)));
        
        type.Name = "NewName";
        
        _repository.UpdateExistingEntity(type);
        
        Assert.NotEqual(initialName, _repository.GetSingleEntityBySpecificationAsync
            (new ProductTypeQueryByIdSpecification(type.Id)).Result.Name);
    }
    
    [Fact]
    public async void UpdateRangeOfExistingEntitiesMethod_Should_UpdateRangeOfEntitiesInDatabase()
    {
        _context = new StoreContext(new DbContextOptionsBuilder<StoreContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);

        _repository = new ProductTypeRepositoryFactory().Create(_context);

        var names = new List<string> { "Name1", "Name2" };
        
        var types = new List<ProductType> { new(names[0]), new(names[1]) };

        await _repository.AddNewRangeOfEntitiesAsync(types);
        
        Assert.Equal(types[0], await _repository.GetSingleEntityBySpecificationAsync
            (new ProductTypeQueryByIdSpecification(types[0].Id)));
        Assert.Equal(types[1], await _repository.GetSingleEntityBySpecificationAsync
            (new ProductTypeQueryByIdSpecification(types[1].Id)));
        
        types[0].Name = "NewName1";
        types[1].Name = "NewName2";
        
        _repository.UpdateRangeOfExistingEntities(types);
        
        Assert.NotEqual(names[0], _repository.GetSingleEntityBySpecificationAsync
            (new ProductTypeQueryByIdSpecification(types[0].Id)).Result.Name);
        Assert.NotEqual(names[1], _repository.GetSingleEntityBySpecificationAsync
            (new ProductTypeQueryByIdSpecification(types[1].Id)).Result.Name);
    }
    
    [Fact]
    public async void RemoveExistingEntityMethod_Should_RemoveEntityFromDatabase()
    {
        _context = new StoreContext(new DbContextOptionsBuilder<StoreContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);

        _repository = new ProductTypeRepositoryFactory().Create(_context);

        var type = new ProductType("Test");

        await _repository.AddNewEntityAsync(type);
        
        Assert.NotEmpty(await _repository.GetAllEntitiesAsync(new ProductTypeQuerySpecification()));
        
        _repository.RemoveExistingEntity(type);
        
        Assert.Empty(await _repository.GetAllEntitiesAsync(new ProductTypeQuerySpecification()));
    }
    
    [Fact]
    public async void RemoveRangeOfExistingEntitiesMethod_Should_RemoveRangeOfEntitiesFromDatabase()
    {
        _context = new StoreContext(new DbContextOptionsBuilder<StoreContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);

        _repository = new ProductTypeRepositoryFactory().Create(_context);

        var names = new List<string> { "Name1", "Name2" };
        
        var types = new List<ProductType> { new(names[0]), new(names[1]) };

        await _repository.AddNewRangeOfEntitiesAsync(types);
        
        Assert.NotEmpty(await _repository.GetAllEntitiesAsync(new ProductTypeQuerySpecification()));
        
        _repository.RemoveRangeOfExistingEntities(types);
        
        Assert.Empty(await _repository.GetAllEntitiesAsync(new ProductTypeQuerySpecification()));
    }
    
    [Fact]
    public async void CountAsyncMethod_Should_ReturnElementsCount()
    {
        _context = new StoreContext(new DbContextOptionsBuilder<StoreContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);

        _repository = new ProductTypeRepositoryFactory().Create(_context);

        var names = new List<string> { "Name1", "Name2" };
        
        var types = new List<ProductType> { new(names[0]), new(names[1]) };

        await _repository.AddNewRangeOfEntitiesAsync(types);
        
        Assert.NotEmpty(await _repository.GetAllEntitiesAsync(new ProductTypeQuerySpecification()));
        
        Assert.Equal(1, await _repository.CountAsync(new ProductTypeQueryByNameSpecification("Name1")));
    }
}