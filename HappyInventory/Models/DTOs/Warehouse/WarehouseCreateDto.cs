using System.ComponentModel.DataAnnotations;

namespace HappyInventory.API.Models.DTOs.Warehouse;

public class WarehouseCreateDto
{
    [Required(ErrorMessage = "Warehouse name is required")]
    [StringLength(100, MinimumLength = 3,
            ErrorMessage = "Name must be between 3-100 characters")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Address is required")]
    [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
    public string Address { get; set; }

    [Required(ErrorMessage = "City is required")]
    [StringLength(50, ErrorMessage = "City cannot exceed 50 characters")]
    public string City { get; set; }

    [Required(ErrorMessage = "Country is required")]
    [StringLength(50, ErrorMessage = "Country cannot exceed 50 characters")]
    public string Country { get; set; }
}
