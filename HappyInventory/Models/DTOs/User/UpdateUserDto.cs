namespace HappyInventory.API.Models.DTOs.User;

public class UpdateUserDto
{
    public string Id { get; set; }
    public string FullName { get; set; }
    public string Role { get; set; }
    public bool Active { get; set; }
}