using System.Linq.Expressions;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.FilteringModels
    .ComputerRelated;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.ComputerRelated;

public class AioComputerQuerySpecification : PersonalComputerQuerySpecification
{
    public AioComputerQuerySpecification(AioComputerFilteringModel filteringModel) : base(filteringModel) =>
        SpecificationCriteria = SpecificationCriteria.And(
            product => GetSpecificationCategoryFilteringModel("Display",
                new[] { "Diagonal", "Resolution", "Matrix type", "Refresh rate" },
                new[]
                {
                    filteringModel.DisplayDiagonal, filteringModel.DisplayResolution,
                    filteringModel.DisplayMatrixType, filteringModel.DisplayRefreshRate
                }, product));
}