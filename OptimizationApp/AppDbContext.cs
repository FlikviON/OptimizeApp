using Microsoft.EntityFrameworkCore;
using OptimizationApp.Models;

namespace OptimizationApp;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<BiomeSetting> BiomeSettings { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BiomeSetting>().HasData(
            new BiomeSetting { Id = 1, Biome = "луг", Speed = 1.0 },
            new BiomeSetting { Id = 2, Biome = "болото", Speed = 0.8 },
            new BiomeSetting { Id = 3, Biome = "горы", Speed = 0.5 }
        );
    }
}