using API.Helpers.DataTransferObjects.ProductRelated;
using API.Helpers.Resolvers;
using AutoMapper;
using Core.Entities.Product.Common.Interfaces;

namespace API.Helpers.DataTransferObjects.Mappings;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMappingProfileForProduct();
    }

    private void CreateMappingProfileForProduct() =>
        CreateMap<IProduct, ProductDto>()
            .ForMember(r => r.Brand, p =>
                p.MapFrom(b => b.Manufacturer.Name))
            .ForMember(r => r.ProductCode, p =>
                p.MapFrom(b => b.ProductCode))
            .ForMember(r => r.Rating, p =>
                p.MapFrom(r => r.Rating.Score))
            .ForMember(r => r.Category, p =>
                p.MapFrom(t => t.ProductType.Name))
            .ForMember(r => r.Images, p =>
                p.MapFrom<ProductUrlResolver>());
}