using HappyInventory.API.Models.Enums;

namespace HappyInventory.API.Models.DTOs.Auth;

public class UserResponseDto
{
    public string Id { get; set; }

    public string Email { get; set; }

    public string FullName { get; set; }

    public UserRole Role { get; set; }

    public bool IsActive { get; set; }
}
