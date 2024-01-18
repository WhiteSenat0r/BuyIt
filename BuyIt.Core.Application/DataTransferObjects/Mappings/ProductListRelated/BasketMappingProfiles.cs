using Application.DataTransferObjects.ProductListRelated;
using Application.DataTransferObjects.ProductListRelated.ListItems;
using AutoMapper;
using Domain.Entities.ProductListRelated;

namespace Application.DataTransferObjects.Mappings.ProductListRelated;

public class BasketMappingProfiles : Profile
{
    public BasketMappingProfiles()
    {
        CreateMappingProfileForBasket();
        CreateMappingProfileForBasketItem();
    }
    
    private void CreateMappingProfileForBasket() =>
        CreateMap<ProductListDto<BasketItemDto>, ProductList<BasketItem>>()
            .ForMember(d => d.Id, i 
                => i.MapFrom(d => d.Id))
            .ForMember(d => d.Items, i 
                => i.MapFrom(d => d.Items));
    
    private void CreateMappingProfileForBasketItem() =>
        CreateMap<BasketItemDto, BasketItem>()
            .ForMember(d => d.Name, p 
                => p.MapFrom(i => i.Name))
            .ForMember(d => d.Price, p 
                => p.MapFrom(i => i.Price))
            .ForMember(d => d.ProductCode, p 
                => p.MapFrom(i => i.ProductCode))
            .ForMember(d => d.Category, p 
                => p.MapFrom(i => i.Category))
            .ForMember(d => d.Quantity, p 
                => p.MapFrom(i => i.Quantity))
            .ForMember(d => d.ImageUrl, p 
                => p.MapFrom(i => i.ImageUrl));
   
}