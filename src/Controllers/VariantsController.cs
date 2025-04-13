using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto;
using WebApi.Models;
using WebApi.Models.Enums;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize(Roles = nameof(UserRole.Admin))]
public class VariantsController : ControllerBase
{
    private readonly IVariantService _variantService;

    public VariantsController(IVariantService variantService)
    {
        _variantService = variantService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductVariant productVariant)
    {
        var result = await _variantService.AddAsync(productVariant);

        return StatusCode(result.StatusCode, result);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_variantService.GetAll());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOne(int id)
    {
        var user = await _variantService.GetOneAsync(u => u.Id == id);

        if (user == null)
            return NotFound(new { message = $"ProductVariant {id} not found" });

        return Ok(user);
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> Update(int id, ProductVariantUpdataDto updatedProductVariant)
    {
        var result = await _variantService.UpdateAsync(id, updatedProductVariant);

        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _variantService.RemoveAsync(id);

        return StatusCode(result.StatusCode, result);
    }
}