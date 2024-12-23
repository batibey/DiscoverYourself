using Microsoft.EntityFrameworkCore;

namespace DiscoverYourself.Managers;

public static class MigrationManager
{
    public static IHost MigrateDatabase<TContext>(this IHost host) where TContext : DbContext
    {
        using var scope = host.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();

        try
        {
            dbContext.Database.Migrate();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Migration failed: {ex.Message}");
            throw ex;
        }

        return host;
    }
}