using Microsoft.EntityFrameworkCore;
using Stofipy.Common.Tests;
using Stofipy.Common.Tests.Seeds;
using Stofipy.DAL.Factories;
using Xunit.Abstractions;

namespace Stofipy.DAL.Tests;

public class DbContextTestsBase : IAsyncLifetime
{
    protected IDbContextFactory<StofipyDbContext> DbContextFactory { get; }
    protected StofipyDbContext DbContextSUT { get; }

    protected DbContextTestsBase(ITestOutputHelper output)
    {
        XUnitTestOutputConverter converter = new(output);
        Console.SetOut(converter);
        
        DbContextFactory = new DbContextSqLiteFactory(GetType().FullName!);
        DbContextSUT = DbContextFactory.CreateDbContext();
    }

    public async Task InitializeAsync()
    {
        await DbContextSUT.Database.EnsureDeletedAsync();
        await DbContextSUT.Database.EnsureCreatedAsync();

        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        dbx
            .SeedTestAuthors()
            .SeedTestAlbums()
            .SeedTestPlaylists()
            .SeedTestFiles()
            .SeedTestFilesInAlbums()
            .SeedTestFilesInPlaylists()
            .SeedTestFilesInQueue()
            .SeedTestProfiles()
            ;
        await dbx.SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        await DbContextSUT.Database.EnsureDeletedAsync();
        await DbContextSUT.DisposeAsync();
    }
}