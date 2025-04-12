namespace HappyInventory.API.Models.DTOs.User;

public class ChangePasswordDto
{
    public string UserId { get; set; }
    public string NewPassword { get; set; }
}