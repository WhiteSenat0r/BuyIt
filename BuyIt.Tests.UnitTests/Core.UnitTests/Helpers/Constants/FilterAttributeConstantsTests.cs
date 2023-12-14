using Application.Helpers.SpecificationResolver.Common.Constants;
using Xunit;

namespace BuyIt.Tests.UnitTests.Core.UnitTests.Helpers.Constants;

public class FilterAttributeConstantsTests
{
    [Fact]
    public void RemovedAttributes_ShouldNotBeNull()
    {
        var filterAttributes = new FilterAttributeConstants();

        var removedAttributes = filterAttributes.RemovedAttributes;

        Assert.NotNull(removedAttributes);
    }
}