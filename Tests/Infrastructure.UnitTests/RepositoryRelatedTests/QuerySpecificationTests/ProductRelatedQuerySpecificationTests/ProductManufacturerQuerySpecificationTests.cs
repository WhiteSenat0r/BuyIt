﻿using Core.Entities.Product;
using Infrastructure.Repositories.Common.QuerySpecifications.Common.Interfaces;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductManufacturerQueries;

namespace Tests.Infrastructure.UnitTests.RepositoryRelatedTests.QuerySpecificationTests.ProductRelatedQuerySpecificationTests;

public class ProductManufacturerQuerySpecificationTests
{
    private IQuerySpecification<ProductManufacturer> _querySpecification = null!;

    [Fact]
    public void ProductQuerySpecificationConstructor_Should_CreateNewQuerySpecificationInstance()
    {
        _querySpecification = new ProductManufacturerQuerySpecification();
        
        Assert.IsType<ProductManufacturerQuerySpecification>(_querySpecification);
        Assert.NotNull(_querySpecification);
    }
    
    [Fact]
    public void ProductQuerySpecificationByIdConstructor_Should_CreateNewQuerySpecificationInstance()
    {
        _querySpecification = new ProductManufacturerQueryByIdSpecification(Guid.NewGuid());
        
        Assert.IsType<ProductManufacturerQueryByIdSpecification>(_querySpecification);
        Assert.NotNull(_querySpecification);
    }
    
    [Fact]
    public void ProductQuerySpecificationByNameConstructor_Should_CreateNewQuerySpecificationInstance()
    {
        _querySpecification = new ProductManufacturerQueryByNameSpecification("Name");
        
        Assert.IsType<ProductManufacturerQueryByNameSpecification>(_querySpecification);
        Assert.NotNull(_querySpecification);
    }
    
    [Fact]
    public void ProductQuerySpecificationByCountryConstructor_Should_CreateNewQuerySpecificationInstance()
    {
        _querySpecification = new ProductManufacturerQueryByCountrySpecification("Country");
        
        Assert.IsType<ProductManufacturerQueryByCountrySpecification>(_querySpecification);
        Assert.NotNull(_querySpecification);
    }
}