namespace HappyInventory.API.Models.Entities;

public class Item
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string? SKUCode { get; set; }

    public int Qty { get; set; }

    public decimal CostPrice { get; set; }

    public decimal? MSRPPrice { get; set; }

    public int WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; }
}