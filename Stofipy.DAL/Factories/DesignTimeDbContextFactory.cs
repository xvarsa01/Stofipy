using Microsoft.EntityFrameworkCore.Design;

namespace Stofipy.DAL.Factories;

/// <summary>
///     EF Core CLI migration generation uses this DbContext to create model and migration
/// </summary>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<StofipyDbContext>
{
    private readonly DbContextSqLiteFactory _dbContextSqLiteFactory = new("stofipy.db", true);

    public StofipyDbContext CreateDbContext(string[] args)
    {
        return _dbContextSqLiteFactory.CreateDbContext();
    }
}
