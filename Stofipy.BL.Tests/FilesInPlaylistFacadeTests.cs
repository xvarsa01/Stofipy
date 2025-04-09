using Microsoft.EntityFrameworkCore;
using Stofipy.BL.Facades;
using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Models;
using Stofipy.Common.Tests.Seeds;
using Stofipy.DAL;
using Stofipy.DAL.Entities;
using Stofipy.DAL.Repositories;
using Xunit.Abstractions;

namespace Stofipy.BL.Tests;

public class FilesInPlaylistFacadeTests : FacadeTestsBase
{
    private readonly IFilesInPlaylistFacade _filesInPlaylistFacade;
    private readonly StofipyDbContext _stofipyDbContext;
    
    public FilesInPlaylistFacadeTests(ITestOutputHelper output) : base(output)
    {
        _stofipyDbContext = DbContextFactory.CreateDbContext();
        var filesInPlaylistRepository = new FilesInPlaylistRepository(_stofipyDbContext);
        _filesInPlaylistFacade = new FilesInPlaylistFacade(filesInPlaylistRepository, FilesInPlaylistModelMapper);
    }

    [Fact]
    public async Task GetAllFilesInPlaylists_ShouldReturnAllFilesInPlaylists()
    {
        var files = await _filesInPlaylistFacade.GetAllAsync();
        Assert.NotEmpty(files);
    }

    [Fact]
    public async Task GetFilesFromPlaylistPaged_ShouldReturnAllFilesFromPlaylistPaged()
    {
        var playlist = PlaylistTestSeeds.Playlist2;
        
        var files = await _filesInPlaylistFacade.GetAllAsync(playlist.Id, 1, 10);
        Assert.True(files.Count == 10);
        
        var filesRemaining = await _filesInPlaylistFacade.GetAllAsync(playlist.Id, 2, 10);
        Assert.True(filesRemaining.Count == 1);
    }

    [Fact]
    public async Task SearchInPlaylistAsync()
    {
        //Arrange
        var playlist = PlaylistTestSeeds.Playlist2;
        string searchTerm = "File0";
        string searchTerm2 = "ile1";
        
        //Act
        var files = await _filesInPlaylistFacade.SearchInPlaylistAsync(playlist.Id, searchTerm);
        var files2 = await _filesInPlaylistFacade.SearchInPlaylistAsync(playlist.Id, searchTerm2);
        //Assert
        Assert.True(files.Count == 9);
        Assert.True(files2.Count == 2);
    }
    
    [Fact]
    public async Task SortPlaylistsByDuration()
    {
        var playlist = PlaylistTestSeeds.Playlist1;
        Assert.True(playlist.FilesInPlaylists.Count == 4);
        
        Assert.Equal(2, FilesInPlaylistsSeeds.FipFileAInPlaylist1.IndexActual);
        Assert.Equal(4, FilesInPlaylistsSeeds.FipFileBInPlaylist1.IndexActual);
        Assert.Equal(3, FilesInPlaylistsSeeds.FipFileCInPlaylist1.IndexActual);
        Assert.Equal(1, FilesInPlaylistsSeeds.FipFileDInPlaylist1.IndexActual);
        
        await _filesInPlaylistFacade.SortByDurationAsync(playlist.Id);
        
        var fileA = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileAInPlaylist1.Id);
        
        var fileB = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileBInPlaylist1.Id);
        
        var fileC = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileCInPlaylist1.Id);
        
        var fileD = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileDInPlaylist1.Id);
        
        Assert.Equal(1, fileA.IndexActual);
        Assert.Equal(2, fileB.IndexActual);
        Assert.Equal(3, fileC.IndexActual);
        Assert.Equal(4, fileD.IndexActual);
    }

    [Fact]
    public async Task SortPlaylistsByTitle()
    {
        var playlist = PlaylistTestSeeds.Playlist1;
        
        Assert.Equal(2, FilesInPlaylistsSeeds.FipFileAInPlaylist1.IndexActual);
        Assert.Equal(4, FilesInPlaylistsSeeds.FipFileBInPlaylist1.IndexActual);
        Assert.Equal(3, FilesInPlaylistsSeeds.FipFileCInPlaylist1.IndexActual);
        Assert.Equal(1, FilesInPlaylistsSeeds.FipFileDInPlaylist1.IndexActual);
        
        await _filesInPlaylistFacade.SortByTitleAsync(playlist.Id);
        
        var fileA = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileAInPlaylist1.Id);
        
        var fileB = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileBInPlaylist1.Id);
        
        var fileC = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileCInPlaylist1.Id);
        
        var fileD = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileDInPlaylist1.Id);
        
        Assert.Equal(1, fileA.IndexActual);
        Assert.Equal(2, fileB.IndexActual);
        Assert.Equal(3, fileC.IndexActual);
        Assert.Equal(4, fileD.IndexActual);
    }
    
    [Fact]
    public async Task SortPlaylistsByArtist()
    {
        var playlist = PlaylistTestSeeds.Playlist1;
        
        Assert.Equal(2, FilesInPlaylistsSeeds.FipFileAInPlaylist1.IndexActual);
        Assert.Equal(4, FilesInPlaylistsSeeds.FipFileBInPlaylist1.IndexActual);
        Assert.Equal(3, FilesInPlaylistsSeeds.FipFileCInPlaylist1.IndexActual);
        Assert.Equal(1, FilesInPlaylistsSeeds.FipFileDInPlaylist1.IndexActual);
        
        await _filesInPlaylistFacade.SortByArtistAsync(playlist.Id);
        
        var fileA = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileAInPlaylist1.Id);
        
        var fileB = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileBInPlaylist1.Id);
        
        var fileC = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileCInPlaylist1.Id);
        
        var fileD = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileDInPlaylist1.Id);
        
        Assert.Equal(1, fileA.IndexActual);
        Assert.Equal(2, fileB.IndexActual);
        Assert.Equal(3, fileC.IndexActual);
        Assert.Equal(4, fileD.IndexActual);
    }
    
    [Fact]
    public async Task SortPlaylistsByAlbum()
    {
        var playlist = PlaylistTestSeeds.Playlist1;
        
        Assert.Equal(2, FilesInPlaylistsSeeds.FipFileAInPlaylist1.IndexActual);
        Assert.Equal(4, FilesInPlaylistsSeeds.FipFileBInPlaylist1.IndexActual);
        Assert.Equal(3, FilesInPlaylistsSeeds.FipFileCInPlaylist1.IndexActual);
        Assert.Equal(1, FilesInPlaylistsSeeds.FipFileDInPlaylist1.IndexActual);
        
        await _filesInPlaylistFacade.SortByAlbumAsync(playlist.Id);
        
        var fileA = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileAInPlaylist1.Id);
        
        var fileB = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileBInPlaylist1.Id);
        
        var fileC = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileCInPlaylist1.Id);
        
        var fileD = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileDInPlaylist1.Id);
        
        Assert.Equal(1, fileA.IndexActual);
        Assert.Equal(2, fileB.IndexActual);
        Assert.Equal(3, fileC.IndexActual);
        Assert.Equal(4, fileD.IndexActual);
    }
    
    [Fact]
    public async Task SortPlaylistsByCustomOrder()
    {
        var playlist = PlaylistTestSeeds.Playlist1;
        
        Assert.Equal(2, FilesInPlaylistsSeeds.FipFileAInPlaylist1.IndexActual);
        Assert.Equal(4, FilesInPlaylistsSeeds.FipFileBInPlaylist1.IndexActual);
        Assert.Equal(3, FilesInPlaylistsSeeds.FipFileCInPlaylist1.IndexActual);
        Assert.Equal(1, FilesInPlaylistsSeeds.FipFileDInPlaylist1.IndexActual);
        
        await _filesInPlaylistFacade.SortByCustomOrderAsync(playlist.Id);
        
        var fileA = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileAInPlaylist1.Id);
        
        var fileB = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileBInPlaylist1.Id);
        
        var fileC = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileCInPlaylist1.Id);
        
        var fileD = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileDInPlaylist1.Id);
        
        Assert.Equal(1, fileA.IndexActual);
        Assert.Equal(2, fileB.IndexActual);
        Assert.Equal(3, fileC.IndexActual);
        Assert.Equal(4, fileD.IndexActual);
    }
}