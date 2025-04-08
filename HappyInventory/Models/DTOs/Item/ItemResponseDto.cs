using System.ComponentModel.DataAnnotations;

namespace HappyInventory.API.Models.DTOs.Item;

public class ItemResponseDto
{
    public int Id { get; set; }

    [Display(Name = "Item Name")]
    public string Name { get; set; }

    [Display(Name = "SKU Code")]
    public string? SKUCode { get; set; }

    [Display(Name = "Quantity")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
    public int Qty { get; set; }

    [Display(Name = "Cost Price")]
    [DataType(DataType.Currency)]
    public decimal CostPrice { get; set; }

    [Display(Name = "Retail Price")]
    [DataType(DataType.Currency)]
    public decimal? MSRPPrice { get; set; }

    [Display(Name = "Profit Margin")]
    [DisplayFormat(DataFormatString = "{0:P2}")]
    public decimal? ProfitMargin =>
        MSRPPrice.HasValue ? (MSRPPrice - CostPrice) / CostPrice : null;

    // Warehouse information
    public int WarehouseId { get; set; }
    public string WarehouseName { get; set; }
    public string WarehouseLocation { get; set; } 

    [Display(Name = "Last Updated")]
    [DataType(DataType.DateTime)]
    public DateTime? LastUpdated { get; set; }

    [Display(Name = "Created On")]
    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; }
}