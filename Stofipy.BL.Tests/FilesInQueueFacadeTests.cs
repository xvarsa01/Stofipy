using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Stofipy.BL.Facades;
using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Mappers;
using Stofipy.Common.Tests.Seeds;
using Stofipy.DAL.Repositories;
using Xunit.Abstractions;

namespace Stofipy.BL.Tests;

public class FilesInQueueFacadeTests : FacadeTestsBase
{
    private readonly IFilesInQueueFacade _filesInQueueFacade;

    public FilesInQueueFacadeTests(ITestOutputHelper output) : base(output)
    {
        var services = new ServiceCollection();

        // Register DbContext (in-memory for tests)
        var dbContext = DbContextFactory.CreateDbContext();
        services.AddSingleton(dbContext);

        // Register real repo and mapper
        services.AddTransient<FilesInQueueRepository>();
        services.AddTransient<PlaylistRepository>();
        services.AddTransient<AlbumRepository>();
        services.AddTransient<AuthorRepository>();
        services.AddTransient<FileRepository>();
        services.AddTransient<FilesInAlbumRepository>();
        services.AddTransient<FilesInPlaylistRepository>();
        services.AddSingleton<FilesInQueueModelMapper>();
        services.AddSingleton<PlaylistModelMapper>();
        services.AddSingleton<FilesInPlaylistModelMapper>();
        services.AddSingleton<FilesInAlbumModelMapper>();
        services.AddSingleton<AlbumModelMapper>();
        services.AddSingleton<AuthorModelMapper>();
        services.AddSingleton<FileModelMapper>();
        
        // Option 2: OR register a real one if you have all dependencies
        services.AddTransient<IPlaylistFacade, PlaylistFacade>();
        services.AddTransient<IAlbumFacade, AlbumFacade>();
        services.AddTransient<IAuthorFacade, AuthorFacade>();
        services.AddTransient<IFileFacade, FileFacade>();
        services.AddTransient<IFilesInAlbumFacade, FilesInAlbumFacade>();
        
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
            .OrderBy(e => e.PriorityQueue)
            .ThenBy(e => e.Index)
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
        var id1 = await _filesInQueueFacade.AddFileToQueue(file1.Id, file1.FileName, file1.Author.AuthorName);
        var id2 = await _filesInQueueFacade.AddFileToQueue(file2.Id, file2.FileName, file2.Author.AuthorName);

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
        var fileForDelete = FilesInQueueTestSeeds.QueueItemP2;
        
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
        
        var expectedIndexes = new[] {1, 2, 3, 1, 2, 3};
        var actualIndexes = filesFromDb
            .OrderBy(f => f.PriorityQueue)
            .ThenBy(f => f.Index)
            .Select(f => f.Index)
            .ToArray();

        Assert.Equal(expectedIndexes, actualIndexes);
    }

    [Fact]
    public async Task RemoveAllFromPriorityQueueAsync_ShouldRemoveAllFromPriorityQueue()
    {
        await CheckQueueBeforeReorder();
        
        //Act
        await _filesInQueueFacade.RemoveAllFromQueue(true);
        
        //Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var queueFromDb = await dbxAssert.FilesInQueue
            .Include(e => e.File)
            .OrderByDescending(e => e.PriorityQueue)
            .ThenBy(e => e.Index)
            .ToListAsync();
        
        var priorityFromDb = queueFromDb.Where(e => e.PriorityQueue).ToList();
        Assert.Equal(3, queueFromDb.Count);
        Assert.Empty(priorityFromDb);
    }

    [Fact]
    public async Task AddPlaylistToQueue_RemovedOtherItemsFromQueue()
    {
        await CheckQueueBeforeReorder();
        
        var playlist = PlaylistTestSeeds.Playlist1;
        Assert.Equal(4, playlist.FilesInPlaylists.Count);
        
        //Act
        await _filesInQueueFacade.AddPlaylistToQueue(playlist.Id, false);
        
        //Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var queueFromDb = await dbxAssert.FilesInQueue
            .Include(e => e.File)
            .ThenInclude(e => e.Author)
            .ToListAsync();
        
        var expectedIndexes = new[] {1, 2, 3, 4, 1, 2, 3, 4};
        var actualIndexes = queueFromDb
            .OrderByDescending(e => e.PriorityQueue)
            .ThenBy(e => e.Index)
            .Select(f => f.Index)
            .ToArray();

        Assert.Equal(expectedIndexes, actualIndexes);
        
        Assert.True(queueFromDb[0].PriorityQueue);
        Assert.True(queueFromDb[1].PriorityQueue);
        Assert.True(queueFromDb[2].PriorityQueue);
        Assert.True(queueFromDb[3].PriorityQueue);
        
        Assert.False(queueFromDb[4].PriorityQueue);
        Assert.False(queueFromDb[5].PriorityQueue);
        Assert.False(queueFromDb[6].PriorityQueue);
        Assert.False(queueFromDb[7].PriorityQueue);
    }
    [Fact]
    public async Task AddPlaylistToQueue_DontShuffle()
    {
        await CheckQueueBeforeReorder();
        var playlist = PlaylistTestSeeds.Playlist2;
        
        await await _filesInQueueFacade.AddPlaylistToQueue(playlist.Id, false);
        
        //Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var filesFromDb = await dbxAssert.FilesInQueue
            .Include(e => e.File)
            .ThenInclude(e => e.Author)
            .OrderByDescending(e => e.PriorityQueue)
            .ThenBy(e => e.Index)
            .ToListAsync();
        
        Assert.Equal(playlist.FilesInPlaylists.Count + 4, filesFromDb.Count);
        Assert.Equal(FileTestSeeds.File05.FileName, filesFromDb[0].File.FileName);
        Assert.Equal(FileTestSeeds.File06.FileName, filesFromDb[1].File.FileName);
        Assert.Equal(FileTestSeeds.File07.FileName, filesFromDb[2].File.FileName);
        Assert.Equal(FileTestSeeds.File08.FileName, filesFromDb[3].File.FileName);
        
        Assert.Equal(FileTestSeeds.File01.FileName, filesFromDb[4].File.FileName);
        Assert.Equal(FileTestSeeds.File02.FileName, filesFromDb[5].File.FileName);
        Assert.Equal(FileTestSeeds.File03.FileName, filesFromDb[6].File.FileName);
        // ...
        Assert.Equal(FileTestSeeds.File10.FileName, filesFromDb[13].File.FileName);
        Assert.Equal(FileTestSeeds.File11.FileName, filesFromDb[14].File.FileName);
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

    [Fact]
    public async Task ReorderMoveToBackInPriority()
    {
        await CheckQueueBeforeReorder();

        //Act
        await _filesInQueueFacade.ReorderQueue(2,4, true, true);
        
        await using var newDbxAssert = await DbContextFactory.CreateDbContextAsync();
        var newQueueFromDb = await newDbxAssert.FilesInQueue
            .Include(e => e.File)
            .OrderByDescending(e => e.PriorityQueue)
            .ThenBy(e => e.Index)
            .ToListAsync();
        
        Assert.Equal(FileTestSeeds.File05.FileName, newQueueFromDb[0].File.FileName);
        Assert.Equal(FileTestSeeds.File07.FileName, newQueueFromDb[1].File.FileName);
        Assert.Equal(FileTestSeeds.File08.FileName, newQueueFromDb[2].File.FileName);
        Assert.Equal(FileTestSeeds.File06.FileName, newQueueFromDb[3].File.FileName);
    }

    [Fact]
    public async Task ReorderMoveToFrontInPriority()
    {
        await CheckQueueBeforeReorder();

        //Act
        await _filesInQueueFacade.ReorderQueue(4,2, true, true);
        
        await using var newDbxAssert = await DbContextFactory.CreateDbContextAsync();
        var newQueueFromDb = await newDbxAssert.FilesInQueue
            .Include(e => e.File)
            .OrderByDescending(e => e.PriorityQueue)
            .ThenBy(e => e.Index)
            .ToListAsync();
        
        Assert.Equal(FileTestSeeds.File05.FileName, newQueueFromDb[0].File.FileName);
        Assert.Equal(FileTestSeeds.File08.FileName, newQueueFromDb[1].File.FileName);
        Assert.Equal(FileTestSeeds.File06.FileName, newQueueFromDb[2].File.FileName);
        Assert.Equal(FileTestSeeds.File07.FileName, newQueueFromDb[3].File.FileName);
    }
    
    [Fact]
    public async Task ReorderMoveFromPriorityToNonPriority()
    {
        await CheckQueueBeforeReorder();

        //Act
        await _filesInQueueFacade.ReorderQueue(4,1, true, false);
        
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var queueFromDb = await dbxAssert.FilesInQueue
            .Include(e => e.File)
            .OrderByDescending(e => e.PriorityQueue)
            .ThenBy(e => e.Index)
            .ToListAsync();
        
        var priorityFromDb = queueFromDb.Where(e => e.PriorityQueue).ToList();
        var nonPriorityFromDb = queueFromDb.Where(e => !e.PriorityQueue).ToList();
        Assert.Equal(7, queueFromDb.Count);
        Assert.Equal(3, priorityFromDb.Count);
        Assert.Equal(4, nonPriorityFromDb.Count);
        
        Assert.Equal(FileTestSeeds.File05.FileName, queueFromDb[0].File.FileName);
        Assert.Equal(FileTestSeeds.File06.FileName, queueFromDb[1].File.FileName);
        Assert.Equal(FileTestSeeds.File07.FileName, queueFromDb[2].File.FileName);
        
        Assert.Equal(FileTestSeeds.File08.FileName, queueFromDb[3].File.FileName);
        Assert.Equal(FileTestSeeds.File01.FileName, queueFromDb[4].File.FileName);
        Assert.Equal(FileTestSeeds.File02.FileName, queueFromDb[5].File.FileName);
        Assert.Equal(FileTestSeeds.File03.FileName, queueFromDb[6].File.FileName);
    }
    [Fact]
    public async Task ReorderMoveFromPriorityToNonPriority2()
    {
        await CheckQueueBeforeReorder();

        //Act
        await _filesInQueueFacade.ReorderQueue(2,2, true, false);
        
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var queueFromDb = await dbxAssert.FilesInQueue
            .Include(e => e.File)
            .OrderByDescending(e => e.PriorityQueue)
            .ThenBy(e => e.Index)
            .ToListAsync();
        
        var priorityFromDb = queueFromDb.Where(e => e.PriorityQueue).ToList();
        var nonPriorityFromDb = queueFromDb.Where(e => !e.PriorityQueue).ToList();
        Assert.Equal(7, queueFromDb.Count);
        Assert.Equal(3, priorityFromDb.Count);
        Assert.Equal(4, nonPriorityFromDb.Count);
        
        Assert.Equal(FileTestSeeds.File05.FileName, queueFromDb[0].File.FileName);
        Assert.Equal(FileTestSeeds.File07.FileName, queueFromDb[1].File.FileName);
        Assert.Equal(FileTestSeeds.File08.FileName, queueFromDb[2].File.FileName);
        
        Assert.Equal(FileTestSeeds.File01.FileName, queueFromDb[3].File.FileName);
        Assert.Equal(FileTestSeeds.File06.FileName, queueFromDb[4].File.FileName);
        Assert.Equal(FileTestSeeds.File02.FileName, queueFromDb[5].File.FileName);
        Assert.Equal(FileTestSeeds.File03.FileName, queueFromDb[6].File.FileName);
    }
    
    [Fact]
    public async Task ReorderMoveFromNonPriorityToPriority()
    {
        await CheckQueueBeforeReorder();

        // P P P P N N N
        // 1 2 3 4 x 2 3
        
        // P P P P P N N
        // 1 2 3 4 x 2 3
        
        //Act
        await _filesInQueueFacade.ReorderQueue(1,5, false, true);
        
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var queueFromDb = await dbxAssert.FilesInQueue
            .Include(e => e.File)
            .OrderByDescending(e => e.PriorityQueue)
            .ThenBy(e => e.Index)
            .ToListAsync();
        
        var priorityFromDb = queueFromDb.Where(e => e.PriorityQueue).ToList();
        var nonPriorityFromDb = queueFromDb.Where(e => !e.PriorityQueue).ToList();
        Assert.Equal(7, queueFromDb.Count);
        Assert.Equal(5, priorityFromDb.Count);
        Assert.Equal(2, nonPriorityFromDb.Count);
        
        Assert.Equal(FileTestSeeds.File05.FileName, queueFromDb[0].File.FileName);
        Assert.Equal(FileTestSeeds.File06.FileName, queueFromDb[1].File.FileName);
        Assert.Equal(FileTestSeeds.File07.FileName, queueFromDb[2].File.FileName);
        Assert.Equal(FileTestSeeds.File08.FileName, queueFromDb[3].File.FileName);
        Assert.Equal(FileTestSeeds.File01.FileName, queueFromDb[4].File.FileName);
        
        Assert.Equal(FileTestSeeds.File02.FileName, queueFromDb[5].File.FileName);
        Assert.Equal(FileTestSeeds.File03.FileName, queueFromDb[6].File.FileName);
        
        Assert.True(queueFromDb[0].PriorityQueue);
        Assert.True(queueFromDb[1].PriorityQueue);
        Assert.True(queueFromDb[2].PriorityQueue);
        Assert.True(queueFromDb[3].PriorityQueue);
        Assert.True(queueFromDb[4].PriorityQueue);
        
        Assert.False(queueFromDb[5].PriorityQueue);
        Assert.False(queueFromDb[6].PriorityQueue);
    }

    [Fact]
    public async Task ReorderMoveFromNonPriorityToPriority2()
    {
        await CheckQueueBeforeReorder();

        // before: P P P P N N N
        //         1 2 3 4 1 x 3
        
        // after:  P P P P P N N
        //         1 x 2 3 4 1 3
        
        //Act
        await _filesInQueueFacade.ReorderQueue(2,2, false, true);
        
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var queueFromDb = await dbxAssert.FilesInQueue
            .Include(e => e.File)
            .OrderByDescending(e => e.PriorityQueue)
            .ThenBy(e => e.Index)
            .ToListAsync();
        
        var priorityFromDb = queueFromDb.Where(e => e.PriorityQueue).ToList();
        var nonPriorityFromDb = queueFromDb.Where(e => !e.PriorityQueue).ToList();
        Assert.Equal(7, queueFromDb.Count);
        Assert.Equal(5, priorityFromDb.Count);
        Assert.Equal(2, nonPriorityFromDb.Count);
        
        Assert.Equal(FileTestSeeds.File05.FileName, queueFromDb[0].File.FileName);
        Assert.Equal(FileTestSeeds.File02.FileName, queueFromDb[1].File.FileName);
        Assert.Equal(FileTestSeeds.File06.FileName, queueFromDb[2].File.FileName);
        Assert.Equal(FileTestSeeds.File07.FileName, queueFromDb[3].File.FileName);
        Assert.Equal(FileTestSeeds.File08.FileName, queueFromDb[4].File.FileName);
        
        Assert.Equal(FileTestSeeds.File01.FileName, queueFromDb[5].File.FileName);
        Assert.Equal(FileTestSeeds.File03.FileName, queueFromDb[6].File.FileName);
        
        Assert.True(queueFromDb[0].PriorityQueue);
        Assert.True(queueFromDb[1].PriorityQueue);
        Assert.True(queueFromDb[2].PriorityQueue);
        Assert.True(queueFromDb[3].PriorityQueue);
        Assert.True(queueFromDb[4].PriorityQueue);
        
        Assert.False(queueFromDb[5].PriorityQueue);
        Assert.False(queueFromDb[6].PriorityQueue);
    }

    private async Task CheckQueueBeforeReorder()
    {
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var queueFromDb = await dbxAssert.FilesInQueue
            .Include(e => e.File)
            .OrderByDescending(e => e.PriorityQueue)
            .ThenBy(e => e.Index)
            .ToListAsync();
        
        var priorityFromDb = queueFromDb.Where(e => e.PriorityQueue).ToList();
        var nonPriorityFromDb = queueFromDb.Where(e => !e.PriorityQueue).ToList();
        Assert.Equal(7, queueFromDb.Count);
        Assert.Equal(4, priorityFromDb.Count);
        Assert.Equal(3, nonPriorityFromDb.Count);
        Assert.Equal(FileTestSeeds.File05.FileName, queueFromDb[0].File.FileName);
        Assert.Equal(FileTestSeeds.File06.FileName, queueFromDb[1].File.FileName);
        Assert.Equal(FileTestSeeds.File07.FileName, queueFromDb[2].File.FileName);
        Assert.Equal(FileTestSeeds.File08.FileName, queueFromDb[3].File.FileName);
        
        Assert.Equal(FileTestSeeds.File01.FileName, queueFromDb[4].File.FileName);
        Assert.Equal(FileTestSeeds.File02.FileName, queueFromDb[5].File.FileName);
        Assert.Equal(FileTestSeeds.File03.FileName, queueFromDb[6].File.FileName);
    }
}