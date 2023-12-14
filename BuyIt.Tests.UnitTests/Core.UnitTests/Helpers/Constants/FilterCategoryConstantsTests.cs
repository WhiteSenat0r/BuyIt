using Application.Helpers.SpecificationResolver.Common.Constants;
using Xunit;

namespace BuyIt.Tests.UnitTests.Core.UnitTests.Helpers.Constants;

public class FilterCategoryConstantsTests
{
    [Fact]
    public void RemovedCategories_ShouldNotBeNull()
    {
        var filterCategories = new FilterCategoryConstants();

        var removedCategories = filterCategories.RemovedCategories;

        Assert.NotNull(removedCategories);
    }
}