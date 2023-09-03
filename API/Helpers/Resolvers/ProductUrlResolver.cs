using API.Helpers.DataTransferObjects.ProductRelated;
using API.Helpers.DataTransferObjects.ProductRelated.Common.Interfaces;
using AutoMapper;
using Core.Entities.Product.Common.Interfaces;

namespace API.Helpers.Resolvers;

public class ProductUrlResolver : IValueResolver<IProduct, IProductDto, IEnumerable<string>>
{
    private readonly IConfiguration _configuration;

    public ProductUrlResolver
        (IConfiguration configuration) => _configuration = configuration;

    public IEnumerable<string> Resolve
        (IProduct source, IProductDto destination, 
            IEnumerable<string> destMember, ResolutionContext context) => 
        !(destination.GetType() == typeof(GeneralizedProductDto)) ?
            source.MainImagesNames.Select
                (path => _configuration["ApiUrl"] + path).ToList() :
            source.MainImagesNames.Select
                (path => _configuration["ApiUrl"] + path).Take(1).ToList();
    
}