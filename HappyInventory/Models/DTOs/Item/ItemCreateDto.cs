using System.ComponentModel.DataAnnotations;

namespace HappyInventory.API.Models.DTOs.Item;

public class ItemCreateDto
{
    [Required(ErrorMessage = "Item name is required")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string Name { get; set; }

    [StringLength(50, ErrorMessage = "SKU cannot exceed 50 characters")]
    [RegularExpression(@"^[A-Z0-9-]+$",
        ErrorMessage = "SKU must contain only uppercase letters, numbers, and hyphens")]
    public string SKU { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
    public int Quantity { get; set; } = 1;

    [Range(0.01, double.MaxValue, ErrorMessage = "Cost must be positive")]
    public decimal CostPrice { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be positive")]
    public decimal? SellingPrice { get; set; }

    [Required(ErrorMessage = "Warehouse ID is required")]
    public int WarehouseId { get; set; }
}