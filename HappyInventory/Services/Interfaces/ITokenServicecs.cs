using Microsoft.AspNetCore.Identity;

namespace HappyInventory.API.Services.Interfaces;

public interface ITokenServicecs
{
    string createToken(IdentityUser user, List<string> Role);
    Task<bool> CheakUserActive(string Email);
}
