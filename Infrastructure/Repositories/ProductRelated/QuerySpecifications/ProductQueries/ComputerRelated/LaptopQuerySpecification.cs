using System.Linq.Expressions;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.FilteringModels
    .ComputerRelated;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.ComputerRelated;

public class LaptopQuerySpecification : PersonalComputerQuerySpecification
{
    public LaptopQuerySpecification(LaptopFilteringModel filteringModel) : base(filteringModel) =>
        SpecificationCriteria = SpecificationCriteria.And(
            product => GetSpecificationCategoryFilteringModel("General", new[] { "Model family" },
                           new[] { filteringModel.ModelFamily }, product) &&
                       GetSpecificationCategoryFilteringModel("Display",
                           new[] { "Diagonal", "Resolution", "Matrix type", "Refresh rate" },
                           new[]
                           {
                               filteringModel.DisplayDiagonal, filteringModel.DisplayResolution,
                               filteringModel.DisplayMatrixType, filteringModel.DisplayRefreshRate
                           }, product));
}