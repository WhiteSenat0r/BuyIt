using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.Classes;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.FilteringModels
    .ComputerRelated;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.ComputerRelated;

public class PersonalComputerQuerySpecification : BasicProductQuerySpecification
{
    public PersonalComputerQuerySpecification(PersonalComputerFilteringModel filteringModel) : base(filteringModel)
    {
        SpecificationCriteria = criteria =>
            GetSpecificationCategoryFilteringModel("General", new[] { "Classification", "Operating system" },
                new[] { filteringModel.Classification, filteringModel.OperatingSystem }, criteria) &&
            GetSpecificationCategoryFilteringModel("Processor",
                new[] { "Manufacturer", "Series", "Quantity of cores" },
                new[]
                {
                    filteringModel.ProcessorBrand, filteringModel.ProcessorSeries,
                    filteringModel.CoresQuantity
                }, criteria) &&
            GetSpecificationCategoryFilteringModel("Graphics card",
                new[] { "Manufacturer", "Series", "Type", "Amount of memory" },
                new[]
                {
                    filteringModel.GraphicsCardBrand, filteringModel.GraphicsCardSeries,
                    filteringModel.GraphicsCardType, filteringModel.GraphicsCardMemoryCapacity
                }, criteria) &&
            GetSpecificationCategoryFilteringModel("Storage", new[] { "Type", "Amount of memory" },
                new[] { filteringModel.StorageType, filteringModel.StorageCapacity }, criteria) &&
            GetSpecificationCategoryFilteringModel("Random access memory", new[] { "Type", "Amount of memory" },
                new[] { filteringModel.RamType, filteringModel.RamCapacity }, criteria);
    }
}