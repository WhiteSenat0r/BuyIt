using Application.Contracts;
using Application.FilteringModels;
using Application.Specifications.ProductSpecifications.ComputerRelatedSpecifications;
using Domain.Contracts.RepositoryRelated;
using Domain.Entities;
using Xunit;

namespace BuyIt.Tests.UnitTests.Core.UnitTests.QuerySpecificationTests.ProductRelatedQuerySpecificationTests.ComputerRelated;

public class ComputerRelatedQuerySpecificationTests
{
    private IFilteringModel _filteringModel = null!;
    private IQuerySpecification<Product> _querySpecification = null!;
    
    [Theory]
    [MemberData(nameof(GetTypesForTesting))]
    public void ComputerRelatedQuerySpecificationConstructor_Should_CreateNewQuerySpecificationInstance
        (Type querySpecificationType, Type filteringModelType)
    {
        _filteringModel = (Activator.CreateInstance(filteringModelType)
            as IFilteringModel)!;
        
        _querySpecification = (Activator.CreateInstance(querySpecificationType, _filteringModel)
            as IQuerySpecification<Product>)!;

        Assert.Equal(querySpecificationType, _querySpecification!.GetType());
        Assert.NotNull(_querySpecification);
    }

    public static List<object[]> GetTypesForTesting()
    {
        return new List<object[]>
        {
            new object[] { typeof(LaptopQuerySpecification), typeof(LaptopFilteringModel) },
            new object[] { typeof(PersonalComputerQuerySpecification), typeof(PersonalComputerFilteringModel) },
            new object[] { typeof(AioComputerQuerySpecification), typeof(AioComputerFilteringModel) }
        };
    }
}