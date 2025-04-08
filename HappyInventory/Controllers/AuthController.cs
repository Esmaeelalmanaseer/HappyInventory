using HappyInventory.API.Helper.ResponseAPI;
using HappyInventory.API.Models.DTOs.Auth;
using HappyInventory.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HappyInventory.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _usermanager;
    private readonly ITokenServicecs _tokenServicecs;
    public AuthController(UserManager<IdentityUser> usermanager, ITokenServicecs tokenServicecs)
    {
        _usermanager = usermanager;
        _tokenServicecs = tokenServicecs;
    }


    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterRequistDto requist)
    {
        var user = new IdentityUser
        {
            Email = requist.Email?.Trim(),
            UserName = requist.Email?.Trim()
        };

        var usercreated = await _usermanager.CreateAsync(user, requist.Password);
        if (usercreated.Succeeded)
        {
            //add role to user
            var identityuser = await _usermanager.AddToRoleAsync(user, "Reader");
            if (identityuser.Succeeded)
            {
                return Ok();
            }
            else
            {
                if (identityuser.Errors.Any())
                {
                    foreach (var err in identityuser.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                }
            }
        }
        else
        {
            if (usercreated.Errors.Any())
            {
                foreach (var err in usercreated.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
        }
        return ValidationProblem();
    }

    [HttpPost]
    [Route("LogIn")]
    public async Task<IActionResult> LoginUser([FromBody] loginRequistDto requiste)
    {
        IdentityUser? finduser = await _usermanager.FindByEmailAsync(requiste.Email);
        if (finduser is null)
        {
            return NotFound($"Not Found{finduser.Email} ");
        }
        var cheackpassword = await _usermanager.CheckPasswordAsync(finduser, requiste.Password);
        if (!cheackpassword)
        {
            return BadRequest("Some Error Plase Chaek Password or Email");
        }
        if(! await _tokenServicecs.CheakUserActive(finduser.Email))
        {
            return BadRequest(new ResponseAPI(400, "Account is locked. Contact support."));
        }
        //Get Role
        var role = await _usermanager.GetRolesAsync(finduser);

        //Create Token and Responce
        var jwttoken = _tokenServicecs.createToken(finduser, role.ToList());
        var responce = new LoginResponcedto
        {
            Email = requiste.Email,
            Roles = role.ToList(),
            Toekn = jwttoken
        };
        return Ok(responce);

    }
}