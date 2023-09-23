﻿using API.Helpers.DataTransferObjects.Manufacturer;
using API.Helpers.DataTransferObjects.ProductRelated;
using API.Helpers.Resolvers;
using AutoMapper;
using Core.Entities.Product.Common.Interfaces;

namespace API.Helpers.DataTransferObjects.Mappings;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMappingProfileForProductInCatalog();
        CreateMappingProfileForSingleProduct();
        CreateMappingProfileForProductBrands();
    }

    private void CreateMappingProfileForProductBrands() =>
        CreateMap<IProductManufacturer, ProductManufacturerDto>()
            .ForMember(n => n.Brand, manufacturer =>
                manufacturer.MapFrom(m => m.Name));
    
    private void CreateMappingProfileForProductInCatalog() =>
        CreateMap<IProduct, GeneralizedProductDto>()
            .ForMember(d => d.Description, p =>
                p.MapFrom<ProductShortDescriptionResolver>())
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
            .ForMember(d => d.ShortDescription, p =>
                p.MapFrom<ProductShortDescriptionResolver>())
            .ForMember(r => r.ProductCode, p =>
                p.MapFrom(b => b.ProductCode))
            .ForMember(r => r.Rating, p =>
                p.MapFrom(r => r.Rating.Score))
            .ForMember(r => r.Images, p =>
                p.MapFrom<ProductUrlResolver>())
            .ForMember(r => r.Specifications, p =>
                p.MapFrom<ProductSpecificationResolver>());
}