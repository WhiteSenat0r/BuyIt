using System.Linq.Expressions;
using Application.FilteringModels;
using Microsoft.IdentityModel.Tokens;

namespace Application.Specifications;

public sealed class LaptopQuerySpecification : PersonalComputerQuerySpecification
{
    public LaptopQuerySpecification(LaptopFilteringModel filteringModel) : base(filteringModel)
    {
        Criteria = Criteria.And(product =>
            (filteringModel.ModelFamily.IsNullOrEmpty() || filteringModel.ModelFamily.Contains(
                product.Specifications
                    .Single(s =>
                        s.SpecificationCategory.Value.Equals("General") && s.SpecificationAttribute.Value.Equals(
                            "Model family")).SpecificationValue.Value.ToLower())) &&
            (filteringModel.DisplayDiagonal.IsNullOrEmpty() || filteringModel.DisplayDiagonal.Contains(
                product.Specifications
                    .Single(s =>
                        s.SpecificationCategory.Value.Equals("Display") && s.SpecificationAttribute.Value.Equals(
                            "Diagonal")).SpecificationValue.Value.ToLower())) &&
            (filteringModel.DisplayResolution.IsNullOrEmpty() || filteringModel.DisplayResolution.Contains(
                product.Specifications
                    .Single(s =>
                        s.SpecificationCategory.Value.Equals("Display") && s.SpecificationAttribute.Value.Equals(
                            "Resolution")).SpecificationValue.Value.ToLower())) &&
            (filteringModel.DisplayMatrixType.IsNullOrEmpty() || filteringModel.DisplayMatrixType.Contains(
                product.Specifications
                    .Single(s =>
                        s.SpecificationCategory.Value.Equals("Display") && s.SpecificationAttribute.Value.Equals(
                            "Matrix type")).SpecificationValue.Value.ToLower())) &&
            (filteringModel.DisplayRefreshRate.IsNullOrEmpty() || filteringModel.DisplayRefreshRate.Contains(
                product.Specifications
                    .Single(s =>
                        s.SpecificationCategory.Value.Equals("Display") && s.SpecificationAttribute.Value.Equals(
                            "Refresh rate")).SpecificationValue.Value.ToLower())));
    }
}