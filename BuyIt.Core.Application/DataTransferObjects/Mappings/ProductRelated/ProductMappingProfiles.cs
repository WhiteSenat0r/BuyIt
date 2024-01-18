using Application.DataTransferObjects.ProductRelated;
using Application.Helpers;
using AutoMapper;
using Domain.Contracts.ProductRelated;

namespace Application.DataTransferObjects.Mappings.ProductRelated;

public class ProductMappingProfiles : Profile
{
    public ProductMappingProfiles()
    {
        CreateMappingProfileForProductInCatalog();
        CreateMappingProfileForSingleProduct();
    }
    
    private void CreateMappingProfileForProductInCatalog() =>
        CreateMap<IProduct, GeneralizedProductDto>()
            .ForMember(r => r.ProductCode, p =>
                p.MapFrom(b => b.ProductCode))
            .ForMember(r => r.Rating, p =>
                p.MapFrom(r => r.Rating.Score))
            .ForMember(r => r.Images, p =>
                p.MapFrom<ProductUrlResolver>())
            .ForMember(b => b.Category, p =>
                p.MapFrom(m => m.ProductType.Name));
    
    private void CreateMappingProfileForSingleProduct() =>
        CreateMap<IProduct, FullProductDto>()
            .ForMember(b => b.Brand, p =>
                p.MapFrom(m => m.Manufacturer.Name))
            .ForMember(d => d.Description, p =>
                p.MapFrom(d => d.Description))
            .ForMember(r => r.ProductCode, p =>
                p.MapFrom(b => b.ProductCode))
            .ForMember(r => r.Rating, p =>
                p.MapFrom(r => r.Rating.Score))
            .ForMember(r => r.Images, p =>
                p.MapFrom<ProductUrlResolver>())
            .ForMember(r => r.Specifications, p =>
                p.MapFrom<ProductSpecificationResolver>())
            .ForMember(r => r.Category, p =>
                p.MapFrom(с => с.ProductType.Name));
}