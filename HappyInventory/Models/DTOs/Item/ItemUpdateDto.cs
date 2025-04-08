using System.ComponentModel.DataAnnotations;

namespace HappyInventory.API.Models.DTOs.Item;

public class ItemUpdateDto: ItemCreateDto
{
    [Required]
    public int Id { get; set; }
}