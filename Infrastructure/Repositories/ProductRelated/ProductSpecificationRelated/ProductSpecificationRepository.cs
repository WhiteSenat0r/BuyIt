﻿using Core.Entities.Product.ProductSpecification;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Common.Classes;

namespace Infrastructure.Repositories.ProductRelated.ProductSpecificationRelated;

public sealed class ProductSpecificationRepository : GenericRepository<ProductSpecification>
{
    internal ProductSpecificationRepository
        (StoreContext dbContext) : base(dbContext) => Context = dbContext;
}