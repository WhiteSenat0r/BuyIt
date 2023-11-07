using Application.Contracts;
using Application.DataTransferObjects.ProductRelated;
using AutoMapper;
using Domain.Contracts.ProductRelated;
using Microsoft.Extensions.Configuration;

namespace Application.Helpers;

public sealed class ProductUrlResolver : IValueResolver<IProduct, IProductDto, IEnumerable<string>>
{
    private readonly IConfiguration _configuration;

    public ProductUrlResolver
        (IConfiguration configuration) => _configuration = configuration;

    public IEnumerable<string> Resolve
        (IProduct source, IProductDto destination, 
            IEnumerable<string> destMember, ResolutionContext context) => 
        destination is not GeneralizedProductDto ?
            source.MainImagesNames.Select
                (path => _configuration["ApiUrl"] + path).ToList() :
            source.MainImagesNames.Select
                (path => _configuration["ApiUrl"] + path).Take(1).ToList();
}