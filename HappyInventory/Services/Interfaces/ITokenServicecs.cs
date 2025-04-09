using HappyInventory.API.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace HappyInventory.API.Services.Interfaces;

public interface ITokenServicecs
{
    string createToken(User user, List<string> Role);
    Task<bool> CheakUserActive(string Email);
}
