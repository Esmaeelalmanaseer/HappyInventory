using AutoMapper;
using HappyInventory.API.Models.DTOs.Item;
using HappyInventory.API.Models.Entities;

namespace HappyInventory.API.Helper.Mapping;

public class ItemProfile : Profile
{
    public ItemProfile()
    {
        // Item -> ItemResponseDto
        CreateMap<Item, ItemResponseDto>()
            .ForMember(dest => dest.WarehouseName,
                opt => opt.MapFrom(src => src.Warehouse.Name))
            .ForMember(dest => dest.WarehouseLocation,
                opt => opt.MapFrom(src => $"{src.Warehouse.City}, {src.Warehouse.Country}"))
            .ForMember(dest => dest.ProfitMargin,
                opt => opt.MapFrom(src =>
                    src.MSRPPrice.HasValue ?
                    (src.MSRPPrice - src.CostPrice) / src.CostPrice :
                    null));

        // ItemCreateDto -> Item
        CreateMap<ItemCreateDto, Item>()
            .ForMember(dest => dest.Warehouse,
                opt => opt.Ignore());

        // ItemUpdateDto -> Item
        CreateMap<ItemUpdateDto, Item>()
            .ForMember(dest => dest.Warehouse,
                opt => opt.Ignore());
    }
}
