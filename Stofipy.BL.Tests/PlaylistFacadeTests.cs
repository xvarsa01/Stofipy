using Stofipy.BL.Facades;
using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Models;
using Stofipy.Common.Tests;
using Stofipy.Common.Tests.Seeds;
using Stofipy.DAL.Repositories;
using Xunit.Abstractions;

namespace Stofipy.BL.Tests;

public class PlaylistFacadeTests : FacadeTestsBase
{
    private readonly IPlaylistFacade _playlistFacade;

    public PlaylistFacadeTests(ITestOutputHelper output) : base(output)
    {
        var stofipyDbContext = DbContextFactory.CreateDbContext();
        var playlistRepository = new PlaylistRepository(stofipyDbContext);
        _playlistFacade = new PlaylistFacade(playlistRepository, PlaylistModelMapper);
    }

    [Fact]
    public async Task Create_WithNonExistingItem_DoesNotThrow()
    {
        var model = new PlaylistDetailModel
        {
            Id = Guid.Empty,
            PlaylistName = "PlaylistName",
            Description = "Description",
            Length = 0,
            PlayCount = 0,
            IsPublic = false,
            CreatedByName = ProfileTestSeeds.User1.Name,
            CreatedById = ProfileTestSeeds.User1.Id,
        };

        var _ = await _playlistFacade.CreateAsync(model);
    }
    
    [Fact]
    public async Task GetAllPlaylists_IsNotEmpty()
    {
        var playlists = await _playlistFacade.GetAllAsync();
        Assert.NotEmpty(playlists);
    }

    [Fact]
    public async Task GetPlaylistById()
    {
        //Arrange
        var detailModel = PlaylistModelMapper.MapToDetailModel(PlaylistTestSeeds.Playlist1);

        //Act
        var returnedModel = await _playlistFacade.GetByIdAsync(detailModel.Id);

        //Assert
        DeepAssert.Equal(detailModel, returnedModel);
    }

    [Fact]
    public async Task Update_DoesNotThrow()
    {
        //Arrange
        var detailModel = PlaylistModelMapper.MapToDetailModel(PlaylistTestSeeds.Playlist1);
        
        //Act
        var updatedModel = detailModel with
        {
            PlaylistName = detailModel.PlaylistName + " Updated",
            Description = detailModel.Description + " Updated",
        };
        await _playlistFacade.UpdateAsync(updatedModel);
        
        //Assert
        var returnedModel = await _playlistFacade.GetByIdAsync(detailModel.Id);
        DeepAssert.Equal(updatedModel, returnedModel);
    }
    
    [Fact]
    public async Task DeleteById_FromSeeded_DoesNotThrow()
    {
        //Arrange & Act & Assert
        await _playlistFacade.DeleteAsync(PlaylistTestSeeds.Playlist1.Id);
        
        var returnedModel = await _playlistFacade.GetByIdAsync(PlaylistTestSeeds.Playlist1.Id);
        Assert.Null(returnedModel);
    }
}