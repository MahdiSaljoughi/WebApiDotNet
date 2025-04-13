using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto;
using WebApi.Dtos;
using WebApi.Models;
using WebApi.Models.Enums;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> Create(User user)
    {
        try
        {
            var result = await _userService.AddAsync(user);
            return Created("Location", result);
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
    }

    [HttpGet]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public IActionResult GetAll()
    {
        return Ok(_userService.GetAll());
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        var result = await _userService.GetCurrentUser();

        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> GetOne(Guid id)
    {
        var user = await _userService.GetOneAsync(u => u.Id == id);

        if (user == null)
            return NotFound(new { message = $"User {id} not found" });

        return Ok(user);
    }

    [HttpPatch("{id:guid}")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> Update(Guid id, UserUpdateDto updatedUser)
    {
        var result = await _userService.UpdateAsync(id, updatedUser);

        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _userService.RemoveAsync(id);

        return StatusCode(result.StatusCode, result);
    }
}