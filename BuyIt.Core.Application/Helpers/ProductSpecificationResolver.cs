﻿using Application.DataTransferObjects.ProductRelated;
using AutoMapper;
using Domain.Contracts.ProductRelated;
using Microsoft.IdentityModel.Tokens;

namespace Application.Helpers;

public sealed class ProductSpecificationResolver : 
    IValueResolver<IProduct, FullProductDto, IDictionary<string, IDictionary<string, string>>>
{
    public IDictionary<string, IDictionary<string, string>> Resolve(
        IProduct source, FullProductDto destination,
        IDictionary<string, IDictionary<string, string>> destMember, ResolutionContext context)
    {
        if (source.Specifications.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(source), "Product has no specifications!");

        var categories = source.Specifications
            .DistinctBy(s => s.SpecificationCategory.Value).Select(c => c.SpecificationCategory.Value);

        IDictionary<string, IDictionary<string, string>> result = new Dictionary<string, IDictionary<string, string>>();

        foreach (var category in categories)
        {
            var attributesAndValues =
                source.Specifications.Where(c => c.SpecificationCategory.Value.Equals(category))
                    .ToDictionary(specification => specification.SpecificationAttribute.Value,
                        specification => specification.SpecificationValue.Value);

            result.Add(category, attributesAndValues);
        }

        return result;
    }
}