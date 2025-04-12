using HappyInventory.API.Models.DTOs.Item;
using HappyInventory.API.Models.DTOs.Warehouse;
using HappyInventory.API.Services.Interfaces;

namespace HappyInventory.API.Services.Immplemnt;

public class DashboardService : IDashboardService
{
    private readonly IWarehouseService _warehouseService;
    private readonly IItemSservice _itemSservice;

    public DashboardService(IWarehouseService warehouseService, IItemSservice itemSservice)
    {
        _warehouseService = warehouseService;
        _itemSservice = itemSservice;
    }

    public async Task<IEnumerable<ItemStatDto>> GetTopHighItems()
    {
        var Item = await _itemSservice.Order(x => x.Qty, true);
        List<ItemStatDto> LstItemStat = Item.Select(x => new ItemStatDto
        {
            Name=x.Name,
            Quantity = x.Qty
        }).ToList();
        return LstItemStat;
    }

    public async Task<IEnumerable<ItemStatDto>> GetTopLowItems()
    {
        var Item = await _itemSservice.Order(x => x.Qty, false);
        List<ItemStatDto> LstItemStat = Item.Select(x => new ItemStatDto
        {
            Name = x.Name,
            Quantity = x.Qty
        }).ToList();
        return LstItemStat;
    }

    public async Task<IEnumerable<WarehouseStatusDto>> GetWarehouseInventoryStatus()
    {
        var LstWarehouse = await _warehouseService.GetAllAsync();
        List<WarehouseStatusDto> LstWarehouseStatusDto = LstWarehouse.Select(x => new WarehouseStatusDto
        {
            ItemCount=x.ItemCount,
            WarehouseName=x.Name
        }).ToList();
        return LstWarehouseStatusDto;
    }
}
