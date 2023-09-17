﻿using Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.FilteringModels.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated.QuerySpecifications.ProductQueries.Common.FilteringModels;

public sealed class ProductSearchFilteringModel : BasicFilteringModel
{
    public string Text { get; set; }
}