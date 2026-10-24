using Microsoft.EntityFrameworkCore;
using SiteSyncTracker.Models;

namespace SiteSyncTracker.Data;

public class AppDbContext : DbContext
{
    public DbSet<InventoryItem> InventoryItems { get; set; } = null!;
    public DbSet<Status> Statuses { get; set; } = null!;
    public DbSet<Location> Locations { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Status>().HasData(
            new Status { Id = 1, StatusName = "In Transit" },
            new Status { Id = 2, StatusName = "Delivered to Yard" },
            new Status { Id = 3, StatusName = "Staged on Level" },
            new Status { Id = 4, StatusName = "Installed" });

        modelBuilder.Entity<Location>().HasData(
            new Location { Id = 1, ZoneName = "Main Laydown Yard" },
            new Location { Id = 2, ZoneName = "Level 1 Structure" });
    }
}
