using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public sealed class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Hobby> Hobbies { get; set; }

    /*public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }*/
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
        optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=rest;User Id=postgres;Password=cSmaos7OCGYK;");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /*modelBuilder.Entity<User>()
            .HasMany(u => u.Hobbies)
            .WithMany(h => h.Users)
            .UsingEntity(j => j.ToTable("UserHobby"));*/
        modelBuilder.Entity<UserHobby>()
            .HasKey(uh => new { uh.UserId, uh.HobbyId });
        modelBuilder.Entity<UserHobby>()
            .HasOne(uh => uh.User)
            .WithMany(uh => uh.Hobbies)
            .HasForeignKey(uh => uh.UserId);
        modelBuilder.Entity<UserHobby>()
            .HasOne(uh => uh.Hobby)
            .WithMany(uh => uh.Users)
            .HasForeignKey(uh => uh.HobbyId);
    }
}