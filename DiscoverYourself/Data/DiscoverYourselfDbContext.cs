using DiscoverYourself.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiscoverYourself.Data;

public class DiscoverYourselfDbContext : DbContext
{
    public DiscoverYourselfDbContext(DbContextOptions<DiscoverYourselfDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}