using Microsoft.EntityFrameworkCore.Design;

namespace Sotify.DAL.Factories;

/// <summary>
///     EF Core CLI migration generation uses this DbContext to create model and migration
/// </summary>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<StofipyDbContext>
{
    private readonly DbContextSqLiteFactory _dbContextSqLiteFactory = new("simt.db", true);

    public StofipyDbContext CreateDbContext(string[] args)
    {
        return _dbContextSqLiteFactory.CreateDbContext();
    }
}
