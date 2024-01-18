using Application.DataTransferObjects.ProductListRelated;
using Application.DataTransferObjects.ProductListRelated.ListItems;
using AutoMapper;
using Domain.Entities.ProductListRelated;

namespace Application.DataTransferObjects.Mappings.ProductListRelated;

public class ComparisonListMappingProfiles : Profile
{
    public ComparisonListMappingProfiles()
    {
        CreateMappingProfileForComparisonList();
        CreateMappingProfileForComparedItem();
    }
    
    private void CreateMappingProfileForComparisonList() =>
        CreateMap<ProductListDto<ComparedItemDto>, ProductList<ComparedItem>>()
            .ForMember(d => d.Id, i 
                => i.MapFrom(d => d.Id))
            .ForMember(d => d.Items, i 
                => i.MapFrom(d => d.Items));
    
    private void CreateMappingProfileForComparedItem() =>
        CreateMap<ComparedItemDto, ComparedItem>()
            .ForMember(d => d.Name, p 
                => p.MapFrom(i => i.Name))
            .ForMember(d => d.ProductCode, p 
                => p.MapFrom(i => i.ProductCode))
            .ForMember(d => d.Category, p 
                => p.MapFrom(i => i.Category))
            .ForMember(d => d.Specifications, p 
                => p.MapFrom(i => i.Specifications))
            .ForMember(d => d.ImageUrl, p 
                => p.MapFrom(i => i.ImageUrl));
   
}