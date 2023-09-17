using System.Linq.Expressions;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.FilteringModels.ComputerRelated;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.ComputerRelatedSpecifications;

public sealed class LaptopQuerySpecification : PersonalComputerQuerySpecification
{
    public LaptopQuerySpecification(LaptopFilteringModel filteringModel) : base(filteringModel)
    {
        Criteria = Criteria.And(product =>
            (string.IsNullOrEmpty(filteringModel.ModelFamily) || product.Specifications
                .Any(s => s.ProductId.Equals(product.Id)
                          && s.SpecificationCategory.Value.Equals("General") &&
                          s.SpecificationAttribute.Value.Equals("Model family")
                          && s.SpecificationValue.Value.ToLower()
                              .Equals(filteringModel.ModelFamily.ToLower().Replace('_', ' ')))) &&
            (string.IsNullOrEmpty(filteringModel.DisplayDiagonal) || product.Specifications
                .Any(s => s.ProductId.Equals(product.Id)
                          && s.SpecificationCategory.Value.Equals("Display") &&
                          s.SpecificationAttribute.Value.Equals("Diagonal")
                          && s.SpecificationValue.Value.ToLower()
                              .Equals(filteringModel.DisplayDiagonal.ToLower().Replace('_', ' ')))) &&
            (string.IsNullOrEmpty(filteringModel.DisplayResolution) || product.Specifications
                .Any(s => s.ProductId.Equals(product.Id)
                          && s.SpecificationCategory.Value.Equals("Display") &&
                          s.SpecificationAttribute.Value.Equals("Resolution")
                          && s.SpecificationValue.Value.ToLower()
                              .Equals(filteringModel.DisplayResolution.ToLower().Replace('_', ' ')))) &&
            (string.IsNullOrEmpty(filteringModel.DisplayMatrixType) || product.Specifications
                .Any(s => s.ProductId.Equals(product.Id)
                          && s.SpecificationCategory.Value.Equals("Display") &&
                          s.SpecificationAttribute.Value.Equals("Matrix type")
                          && s.SpecificationValue.Value.ToLower()
                              .Equals(filteringModel.DisplayMatrixType.ToLower().Replace('_', ' ')))) &&
            (string.IsNullOrEmpty(filteringModel.DisplayRefreshRate) || product.Specifications
                .Any(s => s.ProductId.Equals(product.Id)
                          && s.SpecificationCategory.Value.Equals("Display") &&
                          s.SpecificationAttribute.Value.Equals("Refresh rate")
                          && s.SpecificationValue.Value.ToLower()
                              .Equals(filteringModel.DisplayRefreshRate.ToLower().Replace('_', ' ')))));
    }
}