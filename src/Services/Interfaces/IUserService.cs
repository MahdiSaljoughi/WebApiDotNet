using System.Linq.Expressions;
using WebApi.Dto;
using WebApi.Dtos;
using WebApi.Models;

namespace WebApi.Services.Interfaces;

public interface IUserService
{
    public Task<User> AddAsync(User user);
    
    public IQueryable<User> GetAll();

    public Task<ResponseDto> GetCurrentUser();
    
    public Task<User?> GetOneAsync(Expression<Func<User, bool>> filter);
    
    public Task<ResponseDto> UpdateAsync(Guid id, UserUpdateDto updatedUser);
    
    public Task UpdateRangeAsync(User[] users);
    
    public Task<ResponseDto> RemoveAsync(Guid id);
    
    public Task RemoveRangeAsync(User[] users);
}