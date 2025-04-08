using HappyInventory.API.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HappyInventory.API.Models.Entities;

public class User : IdentityUser
{
    [Required]
    [PersonalData]
    public string FullName { get; set; }

    [Required]
    [PersonalData]
    public UserRole Role { get; set; }

    [Required]
    public bool IsActive { get; set; } = true;
}