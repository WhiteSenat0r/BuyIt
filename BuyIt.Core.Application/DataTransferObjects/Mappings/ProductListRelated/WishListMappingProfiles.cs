using Application.DataTransferObjects.ProductListRelated;
using Application.DataTransferObjects.ProductListRelated.ListItems;
using AutoMapper;
using Domain.Entities.ProductListRelated;

namespace Application.DataTransferObjects.Mappings.ProductListRelated;

public class WishListMappingProfiles : Profile
{
    public WishListMappingProfiles()
    {
        CreateMappingProfileForWishList();
        CreateMappingProfileForWishedItem();
    }
    
    private void CreateMappingProfileForWishList() =>
        CreateMap<ProductListDto<WishedItemDto>, ProductList<WishedItem>>()
            .ForMember(d => d.Id, i 
                => i.MapFrom(d => d.Id))
            .ForMember(d => d.Items, i 
                => i.MapFrom(d => d.Items));
    
    private void CreateMappingProfileForWishedItem() =>
        CreateMap<WishedItemDto, WishedItem>()
            .ForMember(d => d.Name, p 
                => p.MapFrom(i => i.Name))
            .ForMember(d => d.Price, p 
                => p.MapFrom(i => i.Price))
            .ForMember(d => d.ProductCode, p 
                => p.MapFrom(i => i.ProductCode))
            .ForMember(d => d.Category, p 
                => p.MapFrom(i => i.Category))
            .ForMember(d => d.InStock, p 
                => p.MapFrom(i => i.InStock))
            .ForMember(d => d.ImageUrl, p 
                => p.MapFrom(i => i.ImageUrl));
   
}