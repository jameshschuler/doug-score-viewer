using Microsoft.EntityFrameworkCore;

namespace DougScoreViewerAPI.Entities;

public class MyContext: DbContext
{
    public DbSet<DailyScore>? DailyScores { get; init; }
    public DbSet<DougScore>? DougScores { get; init; }
    public DbSet<Vehicle>? Vehicles { get; init; }
    public DbSet<WeekendScore>? WeekendScores { get; init; }

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
    }
    
    // public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    // {
    //     var entries = ChangeTracker.Entries().Where(e => e.Entity is DougScore 
    //                                                      && e.State is EntityState.Added or EntityState.Modified);
    //     
    //     foreach (var entityEntry in entries)
    //     {
    //         if (entityEntry.State == EntityState.Added)
    //         {
    //             ((DougScore)entityEntry.Entity).CreatedAt = DateTime.Now;
    //         }
    //
    //         if (entityEntry.State == EntityState.Modified)
    //         {
    //             ((DougScore)entityEntry.Entity).UpdatedAt = DateTime.Now;
    //         }
    //     }
    //
    //     return await base.SaveChangesAsync(cancellationToken);
    // }
}