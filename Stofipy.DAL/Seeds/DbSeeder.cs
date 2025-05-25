using Microsoft.EntityFrameworkCore;

namespace Stofipy.DAL.Seeds;

public class DbSeeder(IDbContextFactory<StofipyDbContext> dbContextFactory, DALOptions options)
    : IDbSeeder
{
    public void Seed() => SeedAsync(CancellationToken.None).GetAwaiter().GetResult();

    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        await using StofipyDbContext dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        if(options.SeedDemoData)
        {
            dbContext
                .SeedAuthors()
                .SeedAlbums()
                .SeedPlaylists()
                .SeedFiles()
                // .SeedTestFilesInAlbums()
                .SeedFilesInPlaylists()
                .SeedFilesInQueue()
                ;
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
