using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HappyInventory.API.Models.Entities;

public class User : IdentityUser
{
    [Required]
    [PersonalData]
    public string FullName { get; set; }

    [Required]
    public bool IsActive { get; set; } = true;
}