using DiscoverYourself.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiscoverYourself.Data;

public class DiscoverYourselfDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<InvestmentGoal> InvestmentGoals { get; set; }

    public DiscoverYourselfDbContext(DbContextOptions<DiscoverYourselfDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<InvestmentGoal>().HasIndex(u => u.Id).IsUnique();
    }
}