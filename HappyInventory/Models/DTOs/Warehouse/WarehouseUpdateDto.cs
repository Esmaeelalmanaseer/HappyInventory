using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HappyInventory.API.Models.DTOs.Warehouse;

public class WarehouseUpdateDto: WarehouseCreateDto
{
    [Required]
    public int Id { get; set; }
}
