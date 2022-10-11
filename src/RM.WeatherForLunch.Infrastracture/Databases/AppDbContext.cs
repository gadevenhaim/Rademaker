using Microsoft.EntityFrameworkCore;
using RM.WeatherForLunch.Core.Models;
using RM.WeatherForLunch.Infrastracture.Databases.Configurations;
using System.Diagnostics.CodeAnalysis;

namespace RM.WeatherForLunch.Infrastracture.Databases;

public class AppDbContext : DbContext
{
    private const string databaseName = "LunchWeather";

    public DbSet<LunchForcast> LunchForcasts => Set<LunchForcast>();

    public AppDbContext([NotNull]DbContextOptions<AppDbContext> options) : base(options)
    {
        this.Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) { return; }
        optionsBuilder.UseInMemoryDatabase(databaseName);
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new LunchForcastEntityTypeConfiguration().Configure(modelBuilder.Entity<LunchForcast>());
    }
}
