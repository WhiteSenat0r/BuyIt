﻿namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.FilteringModels.Common.Interfaces;

public interface IProductFilteringModel
{
    string BrandName { get; }
    
    decimal? LowerPriceLimit { get; }
    
    decimal? UpperPriceLimit { get; }
    
    string SortingType { get; }
}