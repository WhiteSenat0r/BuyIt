using System.Linq.Expressions;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.Classes;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.FilteringModels
    .ComputerRelated;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.ComputerRelatedSpecifications;

public class PersonalComputerQuerySpecification : BasicProductFilteringQuerySpecification
{
    public PersonalComputerQuerySpecification(PersonalComputerFilteringModel filteringModel)
        : base(filteringModel)
    {
        Criteria = Criteria.And(product =>
            (string.IsNullOrEmpty(filteringModel.Classification) || product.Specifications
                .Any(s =>
                    s.SpecificationCategory.Value.Equals("General") && s.SpecificationAttribute.Value.Equals(
                                                                        "Classification")
                                                                    && s.SpecificationValue.Value.ToLower()
                                                                        .Equals(filteringModel.Classification.ToLower()
                                                                            .Replace('_', ' ')))) &&
            (string.IsNullOrEmpty(filteringModel.OperatingSystem) || product.Specifications
                .Any(s =>
                    s.SpecificationCategory.Value.Equals("General") && s.SpecificationAttribute.Value.Equals(
                                                                        "Operating system")
                                                                    && s.SpecificationValue.Value.ToLower()
                                                                        .Equals(filteringModel.OperatingSystem.ToLower()
                                                                            .Replace('_', ' ')))) &&
            (string.IsNullOrEmpty(filteringModel.ProcessorBrand) || product.Specifications
                .Any(s =>
                    s.SpecificationCategory.Value.Equals("Processor") && s.SpecificationAttribute.Value.Equals(
                                                                          "Manufacturer")
                                                                      && s.SpecificationValue.Value.ToLower()
                                                                          .Equals(filteringModel.ProcessorBrand
                                                                              .ToLower().Replace('_', ' ')))) &&
            (string.IsNullOrEmpty(filteringModel.ProcessorModel) || product.Specifications
                .Any(s =>
                    s.SpecificationCategory.Value.Equals("Processor") && s.SpecificationAttribute.Value.Equals("Model")
                                                                      && s.SpecificationValue.Value.ToLower()
                                                                          .Equals(filteringModel.ProcessorModel
                                                                              .ToLower().Replace('_', ' ')))) &&
            (string.IsNullOrEmpty(filteringModel.ProcessorSeries) || product.Specifications
                .Any(s =>
                    s.SpecificationCategory.Value.Equals("Processor") && s.SpecificationAttribute.Value.Equals("Series")
                                                                      && s.SpecificationValue.Value.ToLower()
                                                                          .Equals(filteringModel.ProcessorSeries
                                                                              .ToLower().Replace('_', ' ')))) &&
            (string.IsNullOrEmpty(filteringModel.CoresQuantity) || product.Specifications
                .Any(s =>
                    s.SpecificationCategory.Value.Equals("Processor") && s.SpecificationAttribute.Value.Equals(
                                                                          "Quantity of cores")
                                                                      && s.SpecificationValue.Value.ToLower()
                                                                          .Equals(filteringModel.CoresQuantity.ToLower()
                                                                              .Replace('_', ' ')))) &&
            (string.IsNullOrEmpty(filteringModel.GraphicsCardType) || product.Specifications
                .Any(s => s.SpecificationCategory.Value.Equals("Graphics card") &&
                          s.SpecificationAttribute.Value.Equals("Type")
                          && s.SpecificationValue.Value.ToLower()
                              .Equals(filteringModel.GraphicsCardType.ToLower().Replace('_', ' ')))) &&
            (string.IsNullOrEmpty(filteringModel.GraphicsCardBrand) || product.Specifications
                .Any(s => s.SpecificationCategory.Value.Equals("Graphics card") &&
                          s.SpecificationAttribute.Value.Equals("Manufacturer")
                          && s.SpecificationValue.Value.ToLower()
                              .Equals(filteringModel.GraphicsCardBrand.ToLower().Replace('_', ' ')))) &&
            (string.IsNullOrEmpty(filteringModel.GraphicsCardSeries) || product.Specifications
                .Any(s => s.SpecificationCategory.Value.Equals("Graphics card") &&
                          s.SpecificationAttribute.Value.Equals("Series")
                          && s.SpecificationValue.Value.ToLower()
                              .Equals(filteringModel.GraphicsCardSeries.ToLower().Replace('_', ' ')))) &&
            (string.IsNullOrEmpty(filteringModel.GraphicsCardModel) || product.Specifications
                .Any(s => s.SpecificationCategory.Value.Equals("Graphics card") &&
                          s.SpecificationAttribute.Value.Equals("Model")
                          && s.SpecificationValue.Value.ToLower()
                              .Equals(filteringModel.GraphicsCardModel.ToLower().Replace('_', ' ')))) &&
            (string.IsNullOrEmpty(filteringModel.GraphicsCardMemoryCapacity) || product.Specifications
                .Any(s => s.SpecificationCategory.Value.Equals("Graphics card") &&
                          s.SpecificationAttribute.Value.Equals("Amount of memory")
                          && s.SpecificationValue.Value.ToLower()
                              .Equals(filteringModel.GraphicsCardMemoryCapacity.ToLower().Replace('_', ' ')))) &&
            (string.IsNullOrEmpty(filteringModel.StorageType) || product.Specifications
                .Any(s =>
                    s.SpecificationCategory.Value.Equals("Storage") && s.SpecificationAttribute.Value.Equals("Type")
                                                                    && s.SpecificationValue.Value.ToLower()
                                                                        .Equals(filteringModel.StorageType.ToLower()
                                                                            .Replace('_', ' ')))) &&
            (string.IsNullOrEmpty(filteringModel.StorageCapacity) || product.Specifications
                .Any(s =>
                    s.SpecificationCategory.Value.Equals("Storage") && s.SpecificationAttribute.Value.Equals(
                                                                        "Amount of memory")
                                                                    && s.SpecificationValue.Value.ToLower()
                                                                        .Equals(filteringModel.StorageCapacity.ToLower()
                                                                            .Replace('_', ' ')))) &&
            (string.IsNullOrEmpty(filteringModel.RamType) || product.Specifications
                .Any(s => s.SpecificationCategory.Value.Equals("Random access memory") &&
                          s.SpecificationAttribute.Value.Equals("Type")
                          && s.SpecificationValue.Value.ToLower()
                              .Equals(filteringModel.RamType.ToLower().Replace('_', ' ')))) &&
            (string.IsNullOrEmpty(filteringModel.RamCapacity) || product.Specifications
                .Any(s => s.SpecificationCategory.Value.Equals("Random access memory") &&
                          s.SpecificationAttribute.Value.Equals("Amount of memory")
                          && s.SpecificationValue.Value.ToLower()
                              .Equals(filteringModel.RamCapacity.ToLower().Replace('_', ' ')))));
    }
}