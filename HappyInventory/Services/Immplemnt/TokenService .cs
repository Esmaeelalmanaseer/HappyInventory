using HappyInventory.API.Models.Entities;
using HappyInventory.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HappyInventory.API.Services.Immplemnt;

public class TokenService : ITokenServicecs
{
    private IConfiguration _configuration;
    private readonly UserManager<User> _usermanger;
    public TokenService(IConfiguration configuration, UserManager<User> usermanger)
    {
        this._configuration = configuration;
        _usermanger = usermanger;
    }

    public async Task<bool> CheakUserActive(string Email)
    {
       User userobj=await _usermanger.FindByEmailAsync(Email);
        if(userobj.IsActive)
           return true;
        return false;
    }

    public string createToken(User user, List<string> Role)
    {
        var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Email, user.Email), 
        new Claim(ClaimTypes.Name, user.Email) ,
    };

        claims.AddRange(Role.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
