using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Stofipy.BL.Facades;
using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Mappers;
using Stofipy.BL.Models;
using Stofipy.Common.Tests.Seeds;
using Stofipy.DAL;
using Stofipy.DAL.Repositories;
using Xunit.Abstractions;

namespace Stofipy.BL.Tests;

public class FilesInQueueFacadeTests : FacadeTestsBase
{
    private readonly IFilesInQueueFacade _filesInQueueFacade;
    private readonly StofipyDbContext _stofipyDbContext;
    
    public FilesInQueueFacadeTests(ITestOutputHelper output) : base(output)
    {
        var services = new ServiceCollection();

        // Register DbContext (in-memory for tests)
        var dbContext = DbContextFactory.CreateDbContext();
        services.AddSingleton(dbContext);

        // Register real repo and mapper
        services.AddTransient<FilesInQueueRepository>();
        services.AddTransient<PlaylistRepository>();
        services.AddTransient<FilesInPlaylistRepository>();
        services.AddSingleton<FilesInQueueModelMapper>();
        services.AddSingleton<PlaylistModelMapper>();
        services.AddSingleton<FilesInPlaylistModelMapper>();
        
        // Option 2: OR register a real one if you have all dependencies
        services.AddTransient<IPlaylistFacade, PlaylistFacade>();
        
        services.AddTransient<IFilesInPlaylistFacade, FilesInPlaylistFacade>();

        // Register your facade under test
        services.AddTransient<IFilesInQueueFacade, FilesInQueueFacade>();

        // Build the DI container
        var provider = services.BuildServiceProvider();

        // Resolve what you need
        _filesInQueueFacade = provider.GetRequiredService<IFilesInQueueFacade>();
    }

    [Fact]
    public async Task GetFilesInQueueAsync_ShouldReturnFilesInQueue()
    {
        var queue = await _filesInQueueFacade.GetAllAsync();

        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var queueFromDb = await dbxAssert.FilesInQueue
            .OrderBy(e => e.Index)
            .Include(e => e.File)
            .ThenInclude(e => e.Author)
            .ToListAsync();
        var modelFromDb = FilesInQueueModelMapper.MapToDetailModel(queueFromDb);
        
        Assert.Equal(queue, modelFromDb);
    }
    
    [Fact]
    public async Task AddFilesToQueueAsync_ShouldAddFileToQueue()
    {
        //Arrange
        var file1 = FileTestSeeds.File01;
        var file2 = FileTestSeeds.File02;

        await using var dbxAssertBefore = await DbContextFactory.CreateDbContextAsync();
        var filesFromDbBefore = await dbxAssertBefore.FilesInQueue.ToListAsync();

        //Act
        var id1 = await _filesInQueueFacade.AddToQueue(file1.Id, file1.FileName, file1.Author.AuthorName);
        var id2 = await _filesInQueueFacade.AddToQueue(file2.Id, file2.FileName, file2.Author.AuthorName);

        //Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var filesFromDb = await dbxAssert.FilesInQueue
            .Include(e => e.File)
            .ThenInclude(e => e.Author)
            .ToListAsync();
        
        var file1FromDb = filesFromDb.Single(e => e.Id == id1);
        var file2FromDb = filesFromDb.Single(e => e.Id == id2);
        var model1FromDb = FilesInQueueModelMapper.MapToDetailModel(file1FromDb);
        var model2FromDb = FilesInQueueModelMapper.MapToDetailModel(file2FromDb);
        
        Assert.Equal(filesFromDbBefore.Count + 2, filesFromDb.Count);
        Assert.Equal(filesFromDbBefore.Max(e => e.Index) +1, model1FromDb.Index);
        Assert.Equal(filesFromDbBefore.Max(e => e.Index) +2, model2FromDb.Index);
    }

    [Fact]
    public async Task RemoveFileFromQueueAsync_ShouldRemoveFileFromQueueAndUpdateIndexes()
    {
        //Arrange
        var fileForDelete = FilesInQueueTestSeeds.QueueItem2;
        
        await using var dbxAssertBefore = await DbContextFactory.CreateDbContextAsync();
        var filesFromDbBefore = await dbxAssertBefore.FilesInQueue.ToListAsync();

        //Act
        await _filesInQueueFacade.DeleteAsync(fileForDelete.Id, fileForDelete.Index);
        
        //Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var filesFromDb = await dbxAssert.FilesInQueue
            .Include(e => e.File)
            .ThenInclude(e => e.Author)
            .ToListAsync();
        
        Assert.False(await dbxAssert.FilesInQueue.AnyAsync(i => i.Id == fileForDelete.Id));
        Assert.Equal(filesFromDbBefore.Count -1, filesFromDb.Count);
        
        var expectedIndexes = new[] { 1, 2, 3 };
        var actualIndexes = filesFromDb
            .OrderBy(f => f.Index)
            .Select(f => f.Index)
            .ToArray();

        Assert.Equal(expectedIndexes, actualIndexes);
    }

    [Fact]
    public async Task AddPlaylistToQueue_RemovedOtherItemsFromQueue()
    {
        var playlist = PlaylistTestSeeds.Playlist1;
        await _filesInQueueFacade.AddPlaylistToQueue(playlist.Id, false);
        
        //Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var filesFromDb = await dbxAssert.FilesInQueue
            .Include(e => e.File)
            .ThenInclude(e => e.Author)
            .ToListAsync();
        
        var expectedIndexes = new[] { 1, 2, 3, 4 };
        var actualIndexes = filesFromDb
            .OrderBy(f => f.Index)
            .Select(f => f.Index)
            .ToArray();

        Assert.Equal(expectedIndexes, actualIndexes);
        Assert.Equal(4, playlist.FilesInPlaylists.Count);
        Assert.Equal(1, filesFromDb[0].Index);
        Assert.Equal(2, filesFromDb[1].Index);
        Assert.Equal(3, filesFromDb[2].Index);
        Assert.Equal(4, filesFromDb[3].Index);
    }
    [Fact]
    public async Task AddPlaylistToQueue_DontShuffle()
    {
        var playlist = PlaylistTestSeeds.Playlist2;
        
        var backgroundTask = await _filesInQueueFacade.AddPlaylistToQueue(playlist.Id, false);
        await backgroundTask;
        
        //Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var filesFromDb = await dbxAssert.FilesInQueue
            .Include(e => e.File)
            .ThenInclude(e => e.Author)
            .OrderBy(e => e.Index)
            .ToListAsync();
        
        Assert.Equal(11, filesFromDb.Count);
        Assert.Equal(FileTestSeeds.File01.FileName, filesFromDb[0].File.FileName);
        Assert.Equal(FileTestSeeds.File02.FileName, filesFromDb[1].File.FileName);
        Assert.Equal(FileTestSeeds.File03.FileName, filesFromDb[2].File.FileName);
        Assert.Equal(FileTestSeeds.File04.FileName, filesFromDb[3].File.FileName);
        Assert.Equal(FileTestSeeds.File05.FileName, filesFromDb[4].File.FileName);
        Assert.Equal(FileTestSeeds.File10.FileName, filesFromDb[9].File.FileName);
        Assert.Equal(FileTestSeeds.File11.FileName, filesFromDb[10].File.FileName);
    }
    
    [Fact]
    public async Task AddPlaylistToQueue_Shuffle_ShouldChangeOrder()
    {
        //Arrange
        var playlist = PlaylistTestSeeds.Playlist2;

        //Act
        await await _filesInQueueFacade.AddPlaylistToQueue(playlist.Id, true);

        await using var dbx1 = await DbContextFactory.CreateDbContextAsync();
        var order1 = await dbx1.FilesInQueue.OrderBy(f => f.Index).Select(f => f.FileId).ToListAsync();

        await InitializeAsync(); // Reset DB between runs

        await await _filesInQueueFacade.AddPlaylistToQueue(playlist.Id, true);

        await using var dbx2 = await DbContextFactory.CreateDbContextAsync();
        var order2 = await dbx2.FilesInQueue.OrderBy(f => f.Index).Select(f => f.FileId).ToListAsync();

        //Assert
        Assert.Equal(order1.Count, order2.Count);
        Assert.NotEqual(order1, order2);
    }
    
    
}