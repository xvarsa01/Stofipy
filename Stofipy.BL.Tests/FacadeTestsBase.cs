using Microsoft.EntityFrameworkCore;
using Stofipy.BL.Mappers;
using Stofipy.Common.Tests;
using Stofipy.Common.Tests.Seeds;
using Stofipy.DAL;
using Stofipy.DAL.Factories;
using Stofipy.DAL.Repositories;
using Xunit.Abstractions;

namespace Stofipy.BL.Tests;

public class FacadeTestsBase : IAsyncLifetime
{
    protected FacadeTestsBase(ITestOutputHelper output)
    {
        XUnitTestOutputConverter converter = new(output);
        Console.SetOut(converter);

        FilesInAlbumModelMapper = new FilesInAlbumModelMapper();
        FilesInPlaylistModelMapper = new FilesInPlaylistModelMapper();
        FilesInQueueModelMapper = new FilesInQueueModelMapper();
        FileModelMapper = new FileModelMapper();
        AlbumModelMapper = new AlbumModelMapper(FilesInAlbumModelMapper);
        PlaylistModelMapper = new PlaylistModelMapper();
        AuthorModelMapper = new AuthorModelMapper(AlbumModelMapper, FileModelMapper);

        DbContextFactory = new DbContextSqLiteFactory(GetType().FullName!);
    }

    protected IDbContextFactory<StofipyDbContext> DbContextFactory { get; }

    protected AlbumModelMapper AlbumModelMapper { get; }
    protected AuthorModelMapper AuthorModelMapper { get; }
    protected FileModelMapper FileModelMapper { get; }
    protected PlaylistModelMapper PlaylistModelMapper { get; }
    protected FilesInAlbumModelMapper FilesInAlbumModelMapper { get; }
    protected FilesInPlaylistModelMapper FilesInPlaylistModelMapper { get; }
    protected FilesInQueueModelMapper FilesInQueueModelMapper { get; }
    

    public async Task InitializeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureDeletedAsync();
        await dbx.Database.EnsureCreatedAsync();
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
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureDeletedAsync();
    }
}
