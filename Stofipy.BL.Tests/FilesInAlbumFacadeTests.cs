using Microsoft.EntityFrameworkCore;
using Stofipy.BL.Facades;
using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Models;
using Stofipy.Common.Tests;
using Stofipy.Common.Tests.Seeds;
using Stofipy.DAL;
using Stofipy.DAL.Repositories;
using Xunit.Abstractions;

namespace Stofipy.BL.Tests;

public class FilesInAlbumFacadeTests : FacadeTestsBase
{
    private readonly IFilesInAlbumFacade _filesInAlbumFacade;
    private readonly StofipyDbContext _stofipyDbContext;
    
    public FilesInAlbumFacadeTests(ITestOutputHelper output) : base(output)
    {
        _stofipyDbContext = DbContextFactory.CreateDbContext();
        var filesInAlbumRepository = new FilesInAlbumRepository(_stofipyDbContext);
        _filesInAlbumFacade = new FilesInAlbumFacade(filesInAlbumRepository, FilesInAlbumModelMapper);
    }
    
    [Fact]
    public async Task GetAllFilesInAlbum_ReturnsException()
    {
        await Assert.ThrowsAsync<NotImplementedException>(async () =>
            await _filesInAlbumFacade.GetAllAsync());
    }

    [Fact]
    public async Task GetAllByAlbumId_ReturnsAllFilesInAlbum()
    {
        var albumId = AlbumTestSeeds.BasicAlbum.Id;
        var filesInAlbum = await _filesInAlbumFacade.GetAllByAlbumIdAsync(albumId);
        
        Assert.Equal(4, filesInAlbum.Count);
    }
    
    [Fact]
    public async Task AddFileToAlbum_ShouldAddFileToAlbum()
    {
        //Arrange
        FilesInAlbumModel model = new()
        {
            Id = Guid.NewGuid(),
            FileId = FileTestSeeds.FileBasic.Id,
            FileName = FileTestSeeds.FileBasic.FileName,
            Length = FileTestSeeds.FileBasic.Length,
            PlayCount = FileTestSeeds.FileBasic.PlayCount,
            Index = 0,
        };

        var album = AlbumTestSeeds.BasicAlbum;
        
        //Act
        await _filesInAlbumFacade.CreateAsync(model, album.Id);
        
        //Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var albumFromDb = await dbxAssert.Albums
            .Include(albumEntity => albumEntity.FilesInAlbums)
            .SingleAsync(e => e.Id == album.Id);
        var fileInAlbumFromDb = await dbxAssert.FilesInAlbums
            .Include(e => e.File)
            .SingleAsync(e => e.Id == model.Id);
        
        DeepAssert.Equal(5, albumFromDb.FilesInAlbums.Count);
        model.Index = 5;
        DeepAssert.Equal(model, FilesInAlbumModelMapper.MapToDetailModel(fileInAlbumFromDb));
    }
    
    [Fact]
    public async Task DeleteFileFromAlbumById_FromSeeded_DoesNotThrow()
    {
        //Arrange
        var fileInAlbumToDelete = FilesInAlbumsSeeds.FileInAlbum3InBasicAlbum;
        
        //Act
        await _filesInAlbumFacade.DeleteAsync(fileInAlbumToDelete.Id);
        
        //Assert
        var returnedModel = await _filesInAlbumFacade.GetByIdAsync(AlbumTestSeeds.AlbumForDelete.Id);
        Assert.Null(returnedModel);
        
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var albumFromDb = await dbxAssert.Albums
            .Include(albumEntity => albumEntity.FilesInAlbums)
            .SingleAsync(e => e.Id == AlbumTestSeeds.BasicAlbum.Id);
        
        Assert.Equal(3, albumFromDb.FilesInAlbums.Count);
        foreach (var file in albumFromDb.FilesInAlbums)
        {
            Assert.NotEqual(4, file.Index);
        }
    }
}