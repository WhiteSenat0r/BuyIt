using System.Linq.Expressions;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.Classes;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.FilteringModels
    .ComputerRelated;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.ComputerRelatedSpecifications;

public class PersonalComputerQuerySpecification : BasicProductFilteringQuerySpecification
{
    public PersonalComputerQuerySpecification(PersonalComputerFilteringModel filteringModel)
        : base(filteringModel)
    {
        Criteria = Criteria.And(product =>
            (filteringModel.Classification.IsNullOrEmpty() || filteringModel.Classification.Contains(
                product.Specifications
                    .Single(s =>
                        s.SpecificationCategory.Value.Equals("General") && s.SpecificationAttribute.Value.Equals(
                            "Classification")).SpecificationValue.Value.ToLower())) &&
            (filteringModel.OperatingSystem.IsNullOrEmpty() || filteringModel.Classification.Contains(product
                .Specifications
                .Single(s =>
                    s.SpecificationCategory.Value.Equals("General") && s.SpecificationAttribute.Value.Equals(
                        "Operating system")).SpecificationValue.Value.ToLower())) &&
            (filteringModel.ProcessorBrand.IsNullOrEmpty() || filteringModel.ProcessorBrand.Contains(product
                .Specifications
                .Single(s =>
                    s.SpecificationCategory.Value.Equals("Processor") && s.SpecificationAttribute.Value.Equals(
                        "Manufacturer")).SpecificationValue.Value.ToLower())) &&
            (filteringModel.ProcessorModel.IsNullOrEmpty() || filteringModel.ProcessorModel.Contains(product
                .Specifications
                .Single(s =>
                    s.SpecificationCategory.Value.Equals("Processor") && s.SpecificationAttribute.Value.Equals(
                        "Model")).SpecificationValue.Value.ToLower())) &&
            (filteringModel.ProcessorSeries.IsNullOrEmpty() || filteringModel.ProcessorSeries.Contains(product
                .Specifications
                .Single(s =>
                    s.SpecificationCategory.Value.Equals("Processor") &&
                    s.SpecificationAttribute.Value.Equals(
                        "Series")).SpecificationValue.Value.ToLower())) &&
            (filteringModel.CoresQuantity.IsNullOrEmpty() || filteringModel.CoresQuantity.Contains(product
                .Specifications
                .Single(s =>
                    s.SpecificationCategory.Value.Equals("Processor") &&
                    s.SpecificationAttribute.Value.Equals(
                        "Quantity of cores")).SpecificationValue.Value.ToLower())) &&
            (filteringModel.GraphicsCardType.IsNullOrEmpty() ||
             filteringModel.GraphicsCardType.Contains(product.Specifications
                 .Single(s =>
                     s.SpecificationCategory.Value.Equals("Graphics card") &&
                     s.SpecificationAttribute.Value.Equals(
                         "Type")).SpecificationValue.Value.ToLower())) &&
            (filteringModel.GraphicsCardBrand.IsNullOrEmpty() ||
             filteringModel.GraphicsCardBrand.Contains(product.Specifications
                 .Single(s =>
                     s.SpecificationCategory.Value.Equals("Graphics card") &&
                     s.SpecificationAttribute.Value.Equals(
                         "Manufacturer")).SpecificationValue.Value.ToLower())) &&
            (filteringModel.GraphicsCardSeries.IsNullOrEmpty() ||
             filteringModel.GraphicsCardSeries.Contains(product.Specifications
                 .Single(s =>
                     s.SpecificationCategory.Value.Equals("Graphics card") &&
                     s.SpecificationAttribute.Value.Equals(
                         "Series")).SpecificationValue.Value.ToLower())) &&
            (filteringModel.GraphicsCardModel.IsNullOrEmpty() ||
             filteringModel.GraphicsCardModel.Contains(product.Specifications
                 .Single(s =>
                     s.SpecificationCategory.Value.Equals("Graphics card") &&
                     s.SpecificationAttribute.Value.Equals(
                         "Model")).SpecificationValue.Value.ToLower())) &&
            (filteringModel.GraphicsCardMemoryCapacity.IsNullOrEmpty() ||
             filteringModel.GraphicsCardMemoryCapacity.Contains(product.Specifications
                 .Single(s =>
                     s.SpecificationCategory.Value.Equals("Graphics card") &&
                     s.SpecificationAttribute.Value.Equals(
                         "Amount of memory")).SpecificationValue.Value.ToLower())) &&
            (filteringModel.StorageType.IsNullOrEmpty() || filteringModel.StorageType.Contains(
                product.Specifications
                    .Single(s =>
                        s.SpecificationCategory.Value.Equals("Storage") &&
                        s.SpecificationAttribute.Value.Equals(
                            "Type")).SpecificationValue.Value.ToLower())) &&
            (filteringModel.StorageCapacity.IsNullOrEmpty() ||
             filteringModel.StorageCapacity.Contains(product.Specifications
                 .Single(s =>
                     s.SpecificationCategory.Value.Equals("Storage") &&
                     s.SpecificationAttribute.Value.Equals(
                         "Amount of memory")).SpecificationValue.Value.ToLower())) &&
            (filteringModel.RamType.IsNullOrEmpty() || filteringModel.RamType.Contains(product
                .Specifications
                .Single(s =>
                    s.SpecificationCategory.Value.Equals("Random access memory") &&
                    s.SpecificationAttribute.Value.Equals(
                        "Type")).SpecificationValue.Value.ToLower())) &&
            (filteringModel.RamCapacity.IsNullOrEmpty() ||
             filteringModel.RamCapacity.Contains(product.Specifications
                 .Single(s =>
                     s.SpecificationCategory.Value.Equals("Random access memory") &&
                     s.SpecificationAttribute.Value.Equals(
                         "Amount of memory")).SpecificationValue.Value.ToLower())));
    }
}