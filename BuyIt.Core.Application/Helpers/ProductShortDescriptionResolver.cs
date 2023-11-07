using Application.Contracts;
using AutoMapper;
using Domain.Contracts.ProductRelated;

namespace Application.Helpers;

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