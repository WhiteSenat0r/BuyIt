using System.Linq.Expressions;
using Application.FilteringModels;
using Microsoft.IdentityModel.Tokens;

namespace Application.Specifications.ProductSpecifications.ComputerRelatedSpecifications;

public sealed class AioComputerQuerySpecification : PersonalComputerQuerySpecification
{
    public AioComputerQuerySpecification(AioComputerFilteringModel filteringModel) : base(filteringModel)
    {
        Criteria = Criteria.And(product =>
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