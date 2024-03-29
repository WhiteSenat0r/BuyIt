﻿using Application.Specifications.ProductTypeSpecifications;
using Domain.Contracts.RepositoryRelated.Relational;
using Domain.Entities.ProductRelated;
using Xunit;

namespace BuyIt.Tests.UnitTests.Core.UnitTests.QuerySpecificationTests.ProductRelatedQuerySpecificationTests;

public class ProductTypeQuerySpecificationTests
{
    private IQuerySpecification<ProductType> _querySpecification = null!;

    [Fact]
    public void ProductTypeQuerySpecificationConstructor_Should_CreateNewQuerySpecificationInstance()
    {
        _querySpecification = new ProductTypeQuerySpecification();
        
        Assert.IsType<ProductTypeQuerySpecification>(_querySpecification);
        Assert.NotNull(_querySpecification);
    }
    
    [Fact]
    public void ProductTypeQueryByIdSpecificationConstructor_Should_CreateNewQuerySpecificationInstance()
    {
        _querySpecification = new ProductTypeQueryByIdSpecification(Guid.NewGuid());
        
        Assert.IsType<ProductTypeQueryByIdSpecification>(_querySpecification);
        Assert.NotNull(_querySpecification);
    }
    
    [Fact]
    public void ProductTypeQueryByNameSpecificationConstructor_Should_CreateNewQuerySpecificationInstance()
    {
        _querySpecification = new ProductTypeQueryByNameSpecification("Name");
        
        Assert.IsType<ProductTypeQueryByNameSpecification>(_querySpecification);
        Assert.NotNull(_querySpecification);
    }
    
    [Fact]
    public void ProductTypeQueryByManufacturerSpecificationConstructor_Should_CreateNewQuerySpecificationInstance()
    {
        _querySpecification = new ProductTypeQueryByManufacturerSpecification(new List<string>());
        
        Assert.IsType<ProductTypeQueryByManufacturerSpecification>(_querySpecification);
        Assert.NotNull(_querySpecification);
    }
}