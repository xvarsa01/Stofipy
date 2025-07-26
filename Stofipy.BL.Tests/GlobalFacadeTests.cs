using Microsoft.Extensions.DependencyInjection;
using Stofipy.BL.Facades;
using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Mappers;
using Stofipy.DAL.Repositories;
using Xunit.Abstractions;

namespace Stofipy.BL.Tests;

public class GlobalFacadeTests : FacadeTestsBase
{
    private readonly IGlobalFacade _globalFacade;

    public GlobalFacadeTests(ITestOutputHelper output) : base(output)
    {
        var services = new ServiceCollection();
        
        // Register DbContext (in-memory for tests)
        var dbContext = DbContextFactory.CreateDbContext();
        services.AddSingleton(dbContext);

        // Register real repo and mapper
        services.AddTransient<FileRepository>();
        services.AddTransient<AuthorRepository>();
        services.AddTransient<AlbumRepository>();
        services.AddTransient<PlaylistRepository>();
        
        services.AddSingleton<FileModelMapper>();
        services.AddSingleton<AuthorModelMapper>();
        services.AddSingleton<AlbumModelMapper>();
        services.AddSingleton<FilesInAlbumModelMapper>();
        services.AddSingleton<PlaylistModelMapper>();
        services.AddSingleton<FilesInPlaylistModelMapper>();

        services.AddTransient<IGlobalFacade, GlobalFacade>();
        
        var provider = services.BuildServiceProvider();
        _globalFacade = provider.GetRequiredService<IGlobalFacade>();
    }

    [Fact]
    public async Task GlobalSearch_Files()
    {
        var result = await _globalFacade.SearchGloballyAsync("Search");

        var files = result.Files;
        var best = result.TopResultFile;

        // Assert that the returned similar files include the expected ones
        Assert.Contains(files, f => f.FileName == "Search");
        Assert.Contains(files, f => f.FileName == "Search2");
        Assert.Contains(files, f => f.FileName == "Serrrch2");
        Assert.Contains(files, f => f.FileName == "serch");
        Assert.Contains(files, f => f.FileName == "se r c h321");

        // Assert the best match is not null and is "Search" (exact match)
        Assert.NotNull(best);
        Assert.Equal("Search", best!.FileName);
    }

    
    [Fact]
    public async Task GlobalSearch_Author()
    {
        var result = await _globalFacade.SearchGloballyAsync("Author");

        var authors = result.Authors;
        var best = result.TopResultAuthor;

        Assert.Contains(authors, f => f.AuthorName == "Test Author");
        Assert.Contains(authors, f => f.AuthorName == "Author U");
        Assert.Contains(authors, f => f.AuthorName == "Author V");
        Assert.Contains(authors, f => f.AuthorName == "Author X");
        Assert.Contains(authors, f => f.AuthorName == "AuthorY");

        Assert.NotNull(best);
        Assert.Equal("AuthorY", best!.AuthorName);
    }
}