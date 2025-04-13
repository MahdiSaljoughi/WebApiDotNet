using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using WebApi.Models.Enums;

namespace WebApi.Models;

[Index(nameof(Phone), IsUnique = true)]
[Index(nameof(UserName), IsUnique = true)]
[Index(nameof(Email), IsUnique = true)]
public class User
{
    [Key] public Guid Id { get; set; }

    [MinLength(3)] [MaxLength(30)] public string? UserName { get; set; }

    [MinLength(3)] [MaxLength(30)] public string? FirstName { get; set; }

    [MinLength(3)] [MaxLength(30)] public string? LastName { get; set; }

    [EmailAddress] public string? Email { get; set; }

    [Required]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "Phone number must be exactly 11 digits.")]
    [MinLength(11)] [MaxLength(11)]
    public required string Phone { get; set; }

    [MinLength(10)] [MaxLength(100)] public string? Address { get; set; }

    public UserRole? Role { get; set; } = UserRole.Customer;

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
}