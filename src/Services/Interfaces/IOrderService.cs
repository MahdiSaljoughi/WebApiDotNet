using System.Linq.Expressions;
using WebApi.Dto;
using WebApi.Models;

namespace WebApi.Services.Interfaces;

public interface IOrderService
{
    public Task<ResponseDto> AddAsync(Order order);
    
    public IQueryable<Order> GetAll();
    
    public Task<ResponseDto> GetCurrentOrder();
    
    public Task<Order?> GetOneAsync(Expression<Func<Order, bool>> filter);
    
    public Task<ResponseDto> UpdateAsync(int id, OrderUpdataDto updatedOrder);
    
    public Task<ResponseDto> RemoveAsync(int id);
}