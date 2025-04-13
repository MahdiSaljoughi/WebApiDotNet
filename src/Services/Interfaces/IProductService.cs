using System.Linq.Expressions;
using WebApi.Dto;
using WebApi.Models;

namespace WebApi.Services.Interfaces;

public interface IProductService
{
    public Task<ResponseDto> AddAsync(Product product);
    
    public IQueryable<Product> GetAll();
    
    public Task<Product?> GetOneAsync(Expression<Func<Product, bool>> filter);
    
    public Task<ResponseDto> UpdateAsync(int id, ProductUpdataDto updatedProduct);
    
    public Task<ResponseDto> RemoveAsync(int id);
}