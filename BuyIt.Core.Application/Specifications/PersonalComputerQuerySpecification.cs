using System.Linq.Expressions;
using Application.FilteringModels;
using Microsoft.IdentityModel.Tokens;

namespace Application.Specifications;

public class PersonalComputerQuerySpecification : BasicProductFilteringQuerySpecification
{
    public PersonalComputerQuerySpecification(PersonalComputerFilteringModel filteringModel)
        : base(filteringModel)
    {
        Criteria = Criteria.And(product =>
            (filteringModel.Classification.IsNullOrEmpty() || filteringModel.Classification.Contains(
                product.Specifications
                    .First(s =>
                        s.SpecificationCategory.Value.Equals("General") && s.SpecificationAttribute.Value.Equals(
                            "Classification")).SpecificationValue.Value)) &&
            (filteringModel.OperatingSystem.IsNullOrEmpty() || filteringModel.OperatingSystem.Contains(product
                .Specifications
                .Single(s =>
                    s.SpecificationCategory.Value.Equals("General") && s.SpecificationAttribute.Value.Equals(
                        "Operating system")).SpecificationValue.Value)) &&
            (filteringModel.ProcessorBrand.IsNullOrEmpty() || filteringModel.ProcessorBrand.Contains(product
                .Specifications
                .Single(s =>
                    s.SpecificationCategory.Value.Equals("Processor") && s.SpecificationAttribute.Value.Equals(
                        "Manufacturer")).SpecificationValue.Value)) &&
            (filteringModel.ProcessorModel.IsNullOrEmpty() || filteringModel.ProcessorModel.Contains(product
                .Specifications
                .Single(s =>
                    s.SpecificationCategory.Value.Equals("Processor") && s.SpecificationAttribute.Value.Equals(
                        "Model")).SpecificationValue.Value)) &&
            (filteringModel.ProcessorSeries.IsNullOrEmpty() || filteringModel.ProcessorSeries.Contains(product
                .Specifications
                .Single(s =>
                    s.SpecificationCategory.Value.Equals("Processor") &&
                    s.SpecificationAttribute.Value.Equals(
                        "Series")).SpecificationValue.Value)) &&
            (filteringModel.CoresQuantity.IsNullOrEmpty() || filteringModel.CoresQuantity.Contains(product
                .Specifications
                .Single(s =>
                    s.SpecificationCategory.Value.Equals("Processor") &&
                    s.SpecificationAttribute.Value.Equals(
                        "Quantity of cores")).SpecificationValue.Value)) &&
            (filteringModel.GraphicsCardType.IsNullOrEmpty() ||
             filteringModel.GraphicsCardType.Contains(product.Specifications
                 .Single(s =>
                     s.SpecificationCategory.Value.Equals("Graphics card") &&
                     s.SpecificationAttribute.Value.Equals(
                         "Type")).SpecificationValue.Value)) &&
            (filteringModel.GraphicsCardBrand.IsNullOrEmpty() ||
             filteringModel.GraphicsCardBrand.Contains(product.Specifications
                 .Single(s =>
                     s.SpecificationCategory.Value.Equals("Graphics card") &&
                     s.SpecificationAttribute.Value.Equals(
                         "Manufacturer")).SpecificationValue.Value)) &&
            (filteringModel.GraphicsCardSeries.IsNullOrEmpty() ||
             filteringModel.GraphicsCardSeries.Contains(product.Specifications
                 .Single(s =>
                     s.SpecificationCategory.Value.Equals("Graphics card") &&
                     s.SpecificationAttribute.Value.Equals(
                         "Series")).SpecificationValue.Value)) &&
            (filteringModel.GraphicsCardModel.IsNullOrEmpty() ||
             filteringModel.GraphicsCardModel.Contains(product.Specifications
                 .Single(s =>
                     s.SpecificationCategory.Value.Equals("Graphics card") &&
                     s.SpecificationAttribute.Value.Equals(
                         "Model")).SpecificationValue.Value)) &&
            (filteringModel.GraphicsCardMemoryCapacity.IsNullOrEmpty() ||
             filteringModel.GraphicsCardMemoryCapacity.Contains(product.Specifications
                 .Single(s =>
                     s.SpecificationCategory.Value.Equals("Graphics card") &&
                     s.SpecificationAttribute.Value.Equals(
                         "Amount of memory")).SpecificationValue.Value)) &&
            (filteringModel.StorageType.IsNullOrEmpty() || filteringModel.StorageType.Contains(
                product.Specifications
                    .Single(s =>
                        s.SpecificationCategory.Value.Equals("Storage") &&
                        s.SpecificationAttribute.Value.Equals(
                            "Type")).SpecificationValue.Value)) &&
            (filteringModel.StorageCapacity.IsNullOrEmpty() ||
             filteringModel.StorageCapacity.Contains(product.Specifications
                 .Single(s =>
                     s.SpecificationCategory.Value.Equals("Storage") &&
                     s.SpecificationAttribute.Value.Equals(
                         "Amount of memory")).SpecificationValue.Value)) &&
            (filteringModel.RamType.IsNullOrEmpty() || filteringModel.RamType.Contains(product
                .Specifications
                .Single(s =>
                    s.SpecificationCategory.Value.Equals("Random access memory") &&
                    s.SpecificationAttribute.Value.Equals(
                        "Type")).SpecificationValue.Value)) &&
            (filteringModel.RamCapacity.IsNullOrEmpty() ||
             filteringModel.RamCapacity.Contains(product.Specifications
                 .Single(s =>
                     s.SpecificationCategory.Value.Equals("Random access memory") &&
                     s.SpecificationAttribute.Value.Equals(
                         "Amount of memory")).SpecificationValue.Value)));
    }
}