using System.Linq.Expressions;
using WebApi.Dto;
using WebApi.Models;

namespace WebApi.Services.Interfaces;

public interface IVariantService
{
    public Task<ResponseDto> AddAsync(ProductVariant variant);
    
    public IQueryable<ProductVariant> GetAll();
    
    public Task<ProductVariant?> GetOneAsync(Expression<Func<ProductVariant, bool>> filter);
    
    public Task<ResponseDto> UpdateAsync(int id, ProductVariantUpdataDto updatedProductVariant);
    
    public Task<ResponseDto> RemoveAsync(int id);
}