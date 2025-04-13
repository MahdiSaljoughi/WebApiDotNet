using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<ProductImage> ProductImages { get; set; }

    public DbSet<ProductOrder> ProductOrders { get; set; }
    
    public DbSet<ProductVariant> ProductVariants { get; set; }
    
    public DbSet<Category> Categories { get; set; }
    
    public DbSet<OtpCode> OtpCodes { get; set; }
}