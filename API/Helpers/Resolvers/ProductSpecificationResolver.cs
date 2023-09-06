using API.Helpers.DataTransferObjects.ProductRelated;
using AutoMapper;
using Core.Entities.Product.Common.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Helpers.Resolvers;

public class ProductSpecificationResolver : IValueResolver<IProduct, FullProductDto, IDictionary<string, IDictionary<string, string>>>
{
    public IDictionary<string, IDictionary<string, string>> Resolve
        (IProduct source, FullProductDto destination, 
            IDictionary<string, IDictionary<string, string>> destMember, ResolutionContext context)
    {
        if (source.Specifications.IsNullOrEmpty()) 
            throw new ArgumentNullException(nameof(source), "Product has no specifications!");
        
        var categories = source.Specifications
            .DistinctBy(s => s.Category).Select(c => c.Category);

        IDictionary<string, IDictionary<string, string>> result = new Dictionary<string, IDictionary<string, string>>();

        foreach (var category in categories)
        {
            var attributesAndValues = 
                source.Specifications.Where(c => c.Category.Equals(category))
                    .ToDictionary(specification => specification.Attribute, specification => specification.Value);

            result.Add(category, attributesAndValues);
        }

        return result;
    }
}