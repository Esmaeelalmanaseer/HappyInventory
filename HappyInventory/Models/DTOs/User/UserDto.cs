namespace HappyInventory.API.Models.DTOs.User;

public class UserDto
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public string Role { get; set; }
    public bool Active { get; set; }
}