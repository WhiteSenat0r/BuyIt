﻿using Application.Specifications.Common;
using Domain.Entities.ProductRelated;

namespace Application.Specifications.ProductTypeSpecifications;

public sealed class ProductTypeQueryByIdSpecification : QuerySpecification<ProductType>
{
    public ProductTypeQueryByIdSpecification(Guid productTypeId) 
        : base (criteria => criteria.Id == productTypeId) { }
}