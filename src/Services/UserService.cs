using System.Linq.Expressions;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Dto;
using WebApi.Dtos;
using WebApi.Exceptions;
using WebApi.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Services;

public class UserService(AppDbContext context, IHttpContextAccessor httpContextAccessor) : IUserService
{
    public async Task<User> AddAsync(User newUser)
    {
        var existingUser = await GetOneAsync(user => user.Phone == newUser.Phone);

        if (existingUser != null)
        {
            throw new DuplicateException($"User {newUser.Phone} already exists.");
        }

        await context.Users.AddAsync(newUser);
        await context.SaveChangesAsync();

        return newUser;
    }

    public IQueryable<User> GetAll()
    {
        return context.Users.AsQueryable();
    }

    public async Task<ResponseDto> GetCurrentUser()
    {
        var userIdClaim = httpContextAccessor.HttpContext?.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
        {
            return new ResponseDto
            {
                Success = false, Message = "User ID not found in token", Data = { }, StatusCode = 404
            };
        }

        var userId = Guid.Parse(userIdClaim.Value);
        var user = await GetOneAsync(u => u.Id == userId);

        if (user == null)
        {
            return new ResponseDto
            {
                Success = false, Message = "User not found", Data = { }, StatusCode = 404
            };
        }

        return new ResponseDto
        {
            Success = true, Message = "User found", Data = user, StatusCode = 200
        };
    }

    public async Task<User?> GetOneAsync(Expression<Func<User, bool>> filter)
    {
        return await context.Users.FirstOrDefaultAsync(filter);
    }

    public async Task<ResponseDto> UpdateAsync(Guid id, UserUpdateDto updatedUser)
    {
        var user = await GetOneAsync(u => u.Id == id);

        if (user == null)
        {
            return new ResponseDto
            {
                Success = false, Message = $"User {id} does not exist.", Data = { },
                StatusCode = 404
            };
        }

        updatedUser.UserName ??= user.UserName;
        updatedUser.Email ??= user.Email;
        updatedUser.Address ??= user.Address;
        updatedUser.FirstName ??= user.FirstName;
        updatedUser.LastName ??= user.LastName;
        updatedUser.Role ??= user.Role;
        user.UpdatedAt = DateTime.UtcNow;

        context.Entry(user).CurrentValues.SetValues(updatedUser);

        await context.SaveChangesAsync();

        return new ResponseDto
        {
            Success = true,
            Message = $"User {user.Id} updated.",
            Data = user,
            StatusCode = 200
        };
    }

    public async Task UpdateRangeAsync(User[] users)
    {
        context.Users.UpdateRange(users);
        await context.SaveChangesAsync();
    }

    public async Task<ResponseDto> RemoveAsync(Guid id)
    {
        var user = await GetOneAsync(u => u.Id == id);

        if (user == null)
        {
            return new ResponseDto
            {
                Success = false, Message = $"User {id} does not exist.", Data = { }, StatusCode = 404
            };
        }

        context.Users.Remove(user);
        await context.SaveChangesAsync();

        return new ResponseDto
        {
            Success = true, Message = $"User {user.Id} deleted successfully", Data = user, StatusCode = 200
        };
    }

    public async Task RemoveRangeAsync(User[] users)
    {
        context.Users.RemoveRange(users);
        await context.SaveChangesAsync();
    }
}