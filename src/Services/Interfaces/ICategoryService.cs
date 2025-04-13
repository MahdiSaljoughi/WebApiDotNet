using System.Linq.Expressions;
using WebApi.Dto;
using WebApi.Models;

namespace WebApi.Services.Interfaces;

public interface ICategoryService
{
    public Task<ResponseDto> AddAsync(Category category);
    
    public IQueryable<Category> GetAll();
    
    public Task<Category?> GetOneAsync(Expression<Func<Category, bool>> filter);
    
    public Task<ResponseDto> UpdateAsync(int id, CategoryUpdataDto updatedCategory);
    
    public Task<ResponseDto> RemoveAsync(int id);
}