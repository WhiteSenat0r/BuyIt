using API.Helpers.DataTransferObjects.ProductRelated.Common.Interfaces;
using API.Helpers.Resolvers.ShortDescriptionResolver.Common.Classes;
using AutoMapper;
using Core.Entities.Product.Common.Interfaces;

namespace API.Helpers.Resolvers;

public sealed class ProductShortDescriptionResolver : IValueResolver<IProduct, IProductDto, string>
{
    public string Resolve
        (IProduct source, IProductDto destination, string destMember, ResolutionContext context) =>
        source.ProductType.Name switch
        {
            "Personal computer" or "All-in-one computer" or "Laptop" =>
                new ComputerShortDescription().GetShortDescription(source),
            _ => throw new ArgumentException
                ("Short description can not be received due to unknown product type!")
        };
}