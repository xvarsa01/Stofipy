using Microsoft.EntityFrameworkCore;
using Stofipy.BL.Facades;
using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Models;
using Stofipy.Common.Tests;
using Stofipy.Common.Tests.Seeds;
using Stofipy.DAL.Repositories;
using Xunit.Abstractions;

namespace Stofipy.BL.Tests;

public class AlbumFacadeTests : FacadeTestsBase
{
    private readonly IAlbumFacade _albumFacade;

    public AlbumFacadeTests(ITestOutputHelper output) : base(output)
    {
        var stofipyDbContext = DbContextFactory.CreateDbContext();
        var albumRepository = new AlbumRepository(stofipyDbContext);
        _albumFacade = new AlbumFacade(albumRepository, AlbumModelMapper);
    }

    [Fact]
    public async Task Create_WithNonExistingItem_DoesNotThrow()
    {
        var model = new AlbumDetailModel
        {
            Id = Guid.NewGuid(),
            AlbumName = "AlbumName",
            Description = "Description",
            Length = 0,
            AuthorId = AuthorTestSeeds.AuthorForFileBasic.Id,
            AuthorName = AuthorTestSeeds.AuthorForFileBasic.AuthorName,
        };

        await _albumFacade.CreateAsync(model);
        
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var albumFromDb = await dbxAssert.Albums
            .Include(e => e.Author)
            .SingleAsync(e => e.Id == model.Id);

        var modelFromDb = AlbumModelMapper.MapToDetailModel(albumFromDb);
        DeepAssert.Equal(model, modelFromDb);
    }
    
    [Fact]
    public async Task GetAlbums_ReturnsException()
    {
        await Assert.ThrowsAsync<NotImplementedException>(async () =>
            await _albumFacade.GetAllAsync());
    }
    
    [Fact]
    public async Task GetAlbumsWithPaging_ReturnsAlbumsPaged()
    {
        var pageNumber = 1;
        const int pageSize = 5;
        var albums = await _albumFacade.GetAllAsync(pageNumber, pageSize);
        pageNumber++;
        var albums2 = await _albumFacade.GetAllAsync(pageNumber, pageSize);
        
        Assert.Equal(pageSize, albums.Count);   // 5 items
        // Assert.Equal(pageSize, albums2.Count);  // 3 more items seeded
        
        var idsPage1 = albums.Select(f => f.Id).ToHashSet();
        var idsPage2 = albums2.Select(f => f.Id).ToHashSet();
        Assert.False(idsPage1.Overlaps(idsPage2));
    }
    
    [Fact]
    public async Task GetAlbumById()
    {
        //Arrange
        var detailModel = AlbumModelMapper.MapToDetailModel(AlbumTestSeeds.BasicAlbum);

        //Act
        var returnedModel = await _albumFacade.GetByIdAsync(detailModel.Id);

        //Assert
        DeepAssert.Equal(detailModel, returnedModel);
    }

    [Fact]
    public async Task Update_DoesNotThrow()
    {
        //Arrange
        var detailModel = AlbumModelMapper.MapToDetailModel(AlbumTestSeeds.AlbumForUpdate);
        
        //Act
        var updatedModel = detailModel with
        {
            AlbumName = detailModel.AlbumName + " Updated",
            Description = detailModel.Description + " Updated",
        };
        await _albumFacade.UpdateAsync(updatedModel);
        
        //Assert
        var returnedModel = await _albumFacade.GetByIdAsync(detailModel.Id);
        DeepAssert.Equal(updatedModel, returnedModel);
    }
    
    [Fact]
    public async Task DeleteById_FromSeeded_DoesNotThrow()
    {
        //Arrange & Act & Assert
        await _albumFacade.DeleteAsync(AlbumTestSeeds.AlbumForDelete.Id);
        
        var returnedModel = await _albumFacade.GetByIdAsync(AlbumTestSeeds.AlbumForDelete.Id);
        Assert.Null(returnedModel);
    }

    [Fact]
    public async Task DeleteById_AlbumUsedAsDefault_Throws()
    {
        var album = AlbumTestSeeds.AlbumM;
        //Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await _albumFacade.DeleteAsync(album.Id));

    }
}