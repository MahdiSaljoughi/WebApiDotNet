using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto;
using WebApi.Models;
using WebApi.Models.Enums;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
       _categoryService = categoryService;
    }

    [HttpPost]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> Create(Category category)
    {
        var result = await _categoryService.AddAsync(category);

        return StatusCode(result.StatusCode, result);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_categoryService.GetAll());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOne(int id)
    {
        var category = await _categoryService.GetOneAsync(c => c.Id == id);

        if (category == null)
            return NotFound(new { message = $"Category {id} not found" });

        return Ok(category);
    }

    [HttpPatch("{id:int}")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> Update(int id, CategoryUpdataDto updatedCategory)
    {
        var result = await _categoryService.UpdateAsync(id, updatedCategory);

        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _categoryService.RemoveAsync(id);

        return StatusCode(result.StatusCode, result);
    }
}