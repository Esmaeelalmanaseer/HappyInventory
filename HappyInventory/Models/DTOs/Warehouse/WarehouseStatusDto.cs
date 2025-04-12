namespace HappyInventory.API.Models.DTOs.Warehouse;

public class WarehouseStatusDto
{
    public string WarehouseName { get; set; } = string.Empty;
    public int ItemCount { get; set; }
}