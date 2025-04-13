using System.ComponentModel.DataAnnotations;
using WebApi.Models.Enums;

namespace WebApi.Dtos;

public class UserCreateDto
{
    [Required]
    public required string Phone { get; set; }
}

public class UserUpdateDto
{
    [MinLength(3)] [MaxLength(30)] public string? UserName { get; set; }

    [MinLength(3)] [MaxLength(30)] public string? FirstName { get; set; }

    [MinLength(3)] [MaxLength(30)] public string? LastName { get; set; }

    [EmailAddress] public string? Email { get; set; }

    [MinLength(10)] [MaxLength(100)] public string? Address { get; set; }

    public UserRole? Role { get; set; }
}