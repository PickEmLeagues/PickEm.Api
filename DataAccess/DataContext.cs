using Microsoft.EntityFrameworkCore;
using PickEm.Api.Domain;

namespace PickEm.Api.DataAccess;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Game> Games { get; set; } = null!;
    public DbSet<League> Leagues { get; set; } = null!;
    public DbSet<GameLeague> Schedules { get; set; } = null!;
    public DbSet<Pick> Picks { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // configure navigation properties for user and league members
        modelBuilder.Entity<User>()
            .HasMany(u => u.OwnedLeagues)
            .WithOne(l => l.Owner)
            .HasForeignKey(l => l.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<User>()
            .HasMany(u => u.MemberLeagues)
            .WithMany(l => l.Members);
    }
}