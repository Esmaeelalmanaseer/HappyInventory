using HappyInventory.API.Helper.ResponseAPI;
using HappyInventory.API.Models.DTOs.Auth;
using HappyInventory.API.Models.DTOs.User;
using HappyInventory.API.Models.Entities;
using HappyInventory.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HappyInventory.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _usermanager;
    private readonly ITokenServicecs _tokenServicecs;
    public AuthController(UserManager<User> usermanager, ITokenServicecs tokenServicecs)
    {
        _usermanager = usermanager;
        _tokenServicecs = tokenServicecs;
    }


    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterRequistDto requist)
    {
        var user = new User
        {
            Email = requist.Email?.Trim(),
            UserName = requist.Email?.Trim(),
            FullName= requist.FullName,
        };

        var usercreated = await _usermanager.CreateAsync(user, requist.Password);
        if (usercreated.Succeeded)
        {
            //add role to user
            var identityuser = await _usermanager.AddToRoleAsync(user, "Auditor");
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
        User? finduser = await _usermanager.FindByEmailAsync(requiste.Email);
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

    [HttpGet("get-all")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = _usermanager.Users.ToList();

        var userList = new List<UserDto>();
        foreach (var user in users)
        {
            var roles = await _usermanager.GetRolesAsync(user);
            userList.Add(new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Role = roles.FirstOrDefault() ?? "",
                Active = user.IsActive
            });
        }

        return Ok(userList);
    }

    [HttpGet("get-by-id/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUserById(string id)
    {
        var user = await _usermanager.FindByIdAsync(id);
        if (user is null) return NotFound();

        var roles = await _usermanager.GetRolesAsync(user);
        return Ok(new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            FullName = user.FullName,
            Role = roles.FirstOrDefault() ?? "",
            Active = user.IsActive
        });
    }
    [HttpPost("create")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateUser([FromBody] RegisterRequistDto req)
    {
        var user = new User
        {
            Email = req.Email,
            UserName = req.Email,
            FullName = req.FullName,
            IsActive = req.IsActive
        };

        var result = await _usermanager.CreateAsync(user, req.Password);
        if (!result.Succeeded) return BadRequest(result.Errors);

        var roleResult = await _usermanager.AddToRoleAsync(user, req.Role);
        if (!roleResult.Succeeded) return BadRequest(roleResult.Errors);

        return Ok();
    }

    [HttpPut("update")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto dto)
    {
        var user = await _usermanager.FindByIdAsync(dto.Id);
        if (user == null) return NotFound();

        user.FullName = dto.FullName;
        user.IsActive = dto.Active;

        var result = await _usermanager.UpdateAsync(user);
        if (!result.Succeeded) return BadRequest(result.Errors);

        var currentRoles = await _usermanager.GetRolesAsync(user);
        await _usermanager.RemoveFromRolesAsync(user, currentRoles);
        await _usermanager.AddToRoleAsync(user, dto.Role);

        return Ok();
    }

    [HttpDelete("delete/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _usermanager.FindByIdAsync(id);
        if (user is null) return NotFound();

        if (user.Email == "admin@happywarehouse.com")
            return BadRequest("Cannot delete admin user");

        IdentityResult? result = await _usermanager.DeleteAsync(user);
        return result.Succeeded ? Ok() : BadRequest(result.Errors);
    }

    [HttpPost("change-password")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
    {
        var user = await _usermanager.FindByIdAsync(dto.UserId);
        if (user == null) return NotFound();

        var token = await _usermanager.GeneratePasswordResetTokenAsync(user);
        var result = await _usermanager.ResetPasswordAsync(user, token, dto.NewPassword);

        return result.Succeeded ? Ok() : BadRequest(result.Errors);
    }


}