using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Helpers;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserData> UserData { get; set; } = null!;

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserData>()
            .HasIndex(ud => new { ud.Email, ud.PhoneNumber })
            .IsUnique();
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Login)
            .IsUnique();
    }
}