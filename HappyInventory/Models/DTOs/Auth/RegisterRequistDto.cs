using System.Diagnostics.Eventing.Reader;

namespace HappyInventory.API.Models.DTOs.Auth;

public class RegisterRequistDto
{
    public string Email { get; set; }
    public string FullName { get; set; }
    public string Password { get; set; }

    public bool IsActive { get; set; } = true;
    public string Role { get;  set; } = "Auditor";
}