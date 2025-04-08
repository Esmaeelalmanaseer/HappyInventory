namespace HappyInventory.API.Models.DTOs.Warehouse;

public class WarehouseResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public int ItemCount { get; set; } 
}
