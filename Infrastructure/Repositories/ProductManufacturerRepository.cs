﻿using Core.Entities.Product;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Common.Classes;

namespace Infrastructure.Repositories;

public class ProductManufacturerRepository : GenericRepository<ProductManufacturer>
{
    internal ProductManufacturerRepository
        (StoreContext dbContext) : base(dbContext) => Context = dbContext;
}