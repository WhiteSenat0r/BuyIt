﻿using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Repositories.Factories.RelationalRepositoryFactories.ProductRelated;
using Persistence.Repositories.ProductRelatedRepositories;
using Xunit;

namespace BuyIt.Tests.UnitTests.Infrastructure.UnitTests.RepositoryRelatedTests.RepositoryFactoryRelatedTests;

public class RepositoryFactoryTests
{
    private StoreContext _dbContext = null!;
    
    [Fact]
    public void ProductRepositoryFactoryClass_Should_CreateNewInstanceAfterCreateMethodWasInvoked()
    {
        _dbContext = new StoreContext(new DbContextOptions<StoreContext>());
        
        var repository = new ProductRepositoryFactory().Create(_dbContext);
        
        Assert.NotNull(repository);
        Assert.IsType<ProductRepository>(repository);
    }
    
    [Fact]
    public void ProductManufacturerRepositoryFactoryClass_Should_CreateNewInstanceAfterCreateMethodWasInvoked()
    {
        _dbContext = new StoreContext(new DbContextOptions<StoreContext>());
        
        var repository = new ProductManufacturerRepositoryFactory().Create(_dbContext);
        
        Assert.NotNull(repository);
        Assert.IsType<ProductManufacturerRepository>(repository);
    }
    
    [Fact]
    public void ProductTypeRepositoryFactoryClass_Should_CreateNewInstanceAfterCreateMethodWasInvoked()
    {
        _dbContext = new StoreContext(new DbContextOptions<StoreContext>());
        
        var repository = new ProductTypeRepositoryFactory().Create(_dbContext);
        
        Assert.NotNull(repository);
        Assert.IsType<ProductTypeRepository>(repository);
    }
    
    [Fact]
    public void ProductRatingRepositoryFactoryClass_Should_CreateNewInstanceAfterCreateMethodWasInvoked()
    {
        _dbContext = new StoreContext(new DbContextOptions<StoreContext>());
        
        var repository = new ProductRatingRepositoryFactory().Create(_dbContext);
        
        Assert.NotNull(repository);
        Assert.IsType<ProductRatingRepository>(repository);
    }
    
    [Fact]
    public void ProductSpecificationRepositoryFactoryClass_Should_CreateNewInstanceAfterCreateMethodWasInvoked()
    {
        _dbContext = new StoreContext(new DbContextOptions<StoreContext>());
        
        var repository = new ProductSpecificationRepositoryFactory().Create(_dbContext);
        
        Assert.NotNull(repository);
        Assert.IsType<ProductSpecificationRepository>(repository);
    }
    
    [Fact]
    public void ProductSpecificationAttributeRepositoryFactoryClass_Should_CreateNewInstanceAfterCreateMethodWasInvoked()
    {
        _dbContext = new StoreContext(new DbContextOptions<StoreContext>());
        
        var repository = new ProductSpecificationAttributeRepositoryFactory().Create(_dbContext);
        
        Assert.NotNull(repository);
        Assert.IsType<ProductSpecificationAttributeRepository>(repository);
    }
    
    [Fact]
    public void ProductSpecificationCategoryRepositoryFactoryClass_Should_CreateNewInstanceAfterCreateMethodWasInvoked()
    {
        _dbContext = new StoreContext(new DbContextOptions<StoreContext>());
        
        var repository = new ProductSpecificationCategoryRepositoryFactory().Create(_dbContext);
        
        Assert.NotNull(repository);
        Assert.IsType<ProductSpecificationCategoryRepository>(repository);
    }
    
    [Fact]
    public void ProductSpecificationValueRepositoryFactoryClass_Should_CreateNewInstanceAfterCreateMethodWasInvoked()
    {
        _dbContext = new StoreContext(new DbContextOptions<StoreContext>());
        
        var repository = new ProductSpecificationValueRepositoryFactory().Create(_dbContext);
        
        Assert.NotNull(repository);
        Assert.IsType<ProductSpecificationValueRepository>(repository);
    }
}