using System.Linq.Expressions;
using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.FilteringModels.ComputerRelated;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.ComputerRelatedSpecifications;

public class LaptopQuerySpecification : PersonalComputerQuerySpecification
{
    public LaptopQuerySpecification(LaptopFilteringModel filteringModel) : base(filteringModel) =>
        Criteria = Criteria.And(product => 
            (string.IsNullOrEmpty(filteringModel.ModelFamily) || product.Specifications
                .Any(s => s.ProductId.Equals(product.Id) 
                          && s.Category.Equals("General") && s.Attribute.Equals("Model family") 
                && s.Value.ToLower().Equals(filteringModel.ModelFamily.ToLower().Replace('_', ' ')))) &&
            
            (string.IsNullOrEmpty(filteringModel.DisplayDiagonal) || product.Specifications
                .Any(s => s.ProductId.Equals(product.Id) 
                          && s.Category.Equals("Display") && s.Attribute.Equals("Diagonal") 
                && s.Value.ToLower().Equals(filteringModel.DisplayDiagonal.ToLower().Replace('_', ' ')))) &&
            
            (string.IsNullOrEmpty(filteringModel.DisplayResolution) || product.Specifications
                .Any(s => s.ProductId.Equals(product.Id) 
                          && s.Category.Equals("Display") && s.Attribute.Equals("Resolution") 
                && s.Value.ToLower().Equals(filteringModel.DisplayResolution.ToLower().Replace('_', ' ')))) &&
            
            (string.IsNullOrEmpty(filteringModel.DisplayMatrixType) || product.Specifications
                .Any(s => s.ProductId.Equals(product.Id) 
                          && s.Category.Equals("Display") && s.Attribute.Equals("Matrix type") 
                && s.Value.ToLower().Equals(filteringModel.DisplayMatrixType.ToLower().Replace('_', ' ')))) &&
            
            (string.IsNullOrEmpty(filteringModel.DisplayRefreshRate) || product.Specifications
                .Any(s => s.ProductId.Equals(product.Id) 
                          && s.Category.Equals("Display") && s.Attribute.Equals("Refresh rate") 
                && s.Value.ToLower().Equals(filteringModel.DisplayRefreshRate.ToLower().Replace('_', ' ')))));
}