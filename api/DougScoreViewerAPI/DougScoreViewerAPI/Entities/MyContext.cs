using Microsoft.EntityFrameworkCore;

namespace DougScoreViewerAPI.Entities;

public class MyContext: DbContext
{
    public DbSet<DougScores>? DougScores { get; init; }

    public MyContext(DbContextOptions<MyContext> options) : base(options)
    {

    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}