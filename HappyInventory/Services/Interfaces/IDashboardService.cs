using HappyInventory.API.Models.DTOs.Item;
using HappyInventory.API.Models.DTOs.Warehouse;

namespace HappyInventory.API.Services.Interfaces;

public interface IDashboardService
{
    Task<IEnumerable<WarehouseStatusDto>> GetWarehouseInventoryStatus();
    Task<IEnumerable<ItemStatDto>> GetTopHighItems();
    Task<IEnumerable<ItemStatDto>> GetTopLowItems();
}
