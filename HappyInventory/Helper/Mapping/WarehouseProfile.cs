using AutoMapper;
using HappyInventory.API.Models.DTOs.Warehouse;
using HappyInventory.API.Models.Entities;

namespace HappyInventory.API.Helper.Mapping;

public class WarehouseProfile : Profile
{
    public WarehouseProfile()
    {
        // Warehouse -> WarehouseResponseDto
        CreateMap<Warehouse, WarehouseResponseDto>()
            .ForMember(dest => dest.ItemCount,
                opt => opt.MapFrom(src => src.Items.Count));

        // WarehouseCreateDto -> Warehouse
        CreateMap<WarehouseCreateDto, Warehouse>()
            .ForMember(dest => dest.Items,
                opt => opt.Ignore());

        // WarehouseUpdateDto -> Warehouse
        CreateMap<WarehouseUpdateDto, Warehouse>()
            .ForMember(dest => dest.Items,
                opt => opt.Ignore());
    }
}