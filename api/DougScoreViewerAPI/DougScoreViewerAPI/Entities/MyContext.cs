using Microsoft.EntityFrameworkCore;

namespace DougScoreViewerAPI.Entities;

public class MyContext: DbContext
{
    public DbSet<DailyScore>? DailyScores { get; init; }
    public DbSet<DougScore>? DougScores { get; init; }
    public DbSet<Vehicle>? Vehicles { get; init; }
    public DbSet<WeekendScore>? WeekendScores { get; init; }
    public DbSet<FeaturedDougScore>? FeaturedDougScores { get; init; }

    public MyContext(DbContextOptions<MyContext> options) : base(options)
    {

    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DougScore>()
            .HasOne(e => e.DailyScore)
            .WithOne(e => e.DougScore)
            .HasForeignKey<DougScore>(e => e.DailyScoreId);

        modelBuilder.Entity<DougScore>()
            .HasOne(e => e.WeekendScore)
            .WithOne(e => e.DougScore)
            .HasForeignKey<DougScore>(e => e.WeekendScoreId);
        
        modelBuilder.Entity<DougScore>()
            .HasOne(e => e.Vehicle)
            .WithOne(e => e.DougScore)
            .HasForeignKey<DougScore>(e => e.VehicleId);

        modelBuilder.Entity<FeaturedDougScore>()
            .HasOne(e => e.DougScore)
            .WithMany(e => e.FeaturedDougScores)
            .HasForeignKey(e => e.DougScoreId);
    }
}