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
    private readonly FilesInPlaylistRepository _filesInPlaylistRepository;
    private readonly StofipyDbContext _stofipyDbContext;
    
    public FilesInPlaylistFacadeTests(ITestOutputHelper output) : base(output)
    {
        _stofipyDbContext = DbContextFactory.CreateDbContext();
        _filesInPlaylistRepository = new FilesInPlaylistRepository(_stofipyDbContext);
        _filesInPlaylistFacade = new FilesInPlaylistFacade(_filesInPlaylistRepository, FilesInPlaylistModelMapper);
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
        
        var files = await _filesInPlaylistFacade.GetAllAsync(1, 10, playlist.Id);
        Assert.True(files.Count == 10);
        
        var filesRemaining = await _filesInPlaylistFacade.GetAllAsync(2, 10, playlist.Id);
        Assert.True(filesRemaining.Count == 1);
    }

    
    [Fact]
    public async Task SortPlaylistsByDuration()
    {
        var playlist = PlaylistTestSeeds.Playlist1;
        Assert.True(playlist.FilesInPlaylists.Count == 4);
        
        Assert.Equal(FilesInPlaylistsSeeds.FipFileAInPlaylist1.IndexActual, 2);
        Assert.Equal(FilesInPlaylistsSeeds.FipFileBInPlaylist1.IndexActual, 4);
        Assert.Equal(FilesInPlaylistsSeeds.FipFileCInPlaylist1.IndexActual, 3);
        Assert.Equal(FilesInPlaylistsSeeds.FipFileDInPlaylist1.IndexActual, 1);
        
        await _filesInPlaylistFacade.SortByDurationAsync(playlist.Id);
        
        var fileA = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileAInPlaylist1.Id);
        
        var fileB = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileBInPlaylist1.Id);
        
        var fileC = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileCInPlaylist1.Id);
        
        var fileD = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileDInPlaylist1.Id);
        
        Assert.Equal(fileA.IndexActual, 1);
        Assert.Equal(fileB.IndexActual, 2);
        Assert.Equal(fileC.IndexActual, 3);
        Assert.Equal(fileD.IndexActual, 4);
    }

    [Fact]
    public async Task SortPlaylistsByTitle()
    {
        var playlist = PlaylistTestSeeds.Playlist1;
        
        Assert.Equal(FilesInPlaylistsSeeds.FipFileAInPlaylist1.IndexActual, 2);
        Assert.Equal(FilesInPlaylistsSeeds.FipFileBInPlaylist1.IndexActual, 4);
        Assert.Equal(FilesInPlaylistsSeeds.FipFileCInPlaylist1.IndexActual, 3);
        Assert.Equal(FilesInPlaylistsSeeds.FipFileDInPlaylist1.IndexActual, 1);
        
        await _filesInPlaylistFacade.SortByTitleAsync(playlist.Id);
        
        var fileA = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileAInPlaylist1.Id);
        
        var fileB = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileBInPlaylist1.Id);
        
        var fileC = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileCInPlaylist1.Id);
        
        var fileD = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileDInPlaylist1.Id);
        
        Assert.Equal(fileA.IndexActual, 1);
        Assert.Equal(fileB.IndexActual, 2);
        Assert.Equal(fileC.IndexActual, 3);
        Assert.Equal(fileD.IndexActual, 4);
    }
    
    [Fact]
    public async Task SortPlaylistsByArtist()
    {
        var playlist = PlaylistTestSeeds.Playlist1;
        
        Assert.Equal(FilesInPlaylistsSeeds.FipFileAInPlaylist1.IndexActual, 2);
        Assert.Equal(FilesInPlaylistsSeeds.FipFileBInPlaylist1.IndexActual, 4);
        Assert.Equal(FilesInPlaylistsSeeds.FipFileCInPlaylist1.IndexActual, 3);
        Assert.Equal(FilesInPlaylistsSeeds.FipFileDInPlaylist1.IndexActual, 1);
        
        await _filesInPlaylistFacade.SortByArtistAsync(playlist.Id);
        
        var fileA = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileAInPlaylist1.Id);
        
        var fileB = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileBInPlaylist1.Id);
        
        var fileC = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileCInPlaylist1.Id);
        
        var fileD = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileDInPlaylist1.Id);
        
        Assert.Equal(fileA.IndexActual, 1);
        Assert.Equal(fileB.IndexActual, 2);
        Assert.Equal(fileC.IndexActual, 3);
        Assert.Equal(fileD.IndexActual, 4);
    }
    
    [Fact]
    public async Task SortPlaylistsByAlbum()
    {
        var playlist = PlaylistTestSeeds.Playlist1;
        
        Assert.Equal(FilesInPlaylistsSeeds.FipFileAInPlaylist1.IndexActual, 2);
        Assert.Equal(FilesInPlaylistsSeeds.FipFileBInPlaylist1.IndexActual, 4);
        Assert.Equal(FilesInPlaylistsSeeds.FipFileCInPlaylist1.IndexActual, 3);
        Assert.Equal(FilesInPlaylistsSeeds.FipFileDInPlaylist1.IndexActual, 1);
        
        await _filesInPlaylistFacade.SortByAlbumAsync(playlist.Id);
        
        var fileA = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileAInPlaylist1.Id);
        
        var fileB = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileBInPlaylist1.Id);
        
        var fileC = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileCInPlaylist1.Id);
        
        var fileD = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileDInPlaylist1.Id);
        
        Assert.Equal(fileA.IndexActual, 1);
        Assert.Equal(fileB.IndexActual, 2);
        Assert.Equal(fileC.IndexActual, 3);
        Assert.Equal(fileD.IndexActual, 4);
    }
    
    [Fact]
    public async Task SortPlaylistsByCustomOrder()
    {
        var playlist = PlaylistTestSeeds.Playlist1;
        
        Assert.Equal(FilesInPlaylistsSeeds.FipFileAInPlaylist1.IndexActual, 2);
        Assert.Equal(FilesInPlaylistsSeeds.FipFileBInPlaylist1.IndexActual, 4);
        Assert.Equal(FilesInPlaylistsSeeds.FipFileCInPlaylist1.IndexActual, 3);
        Assert.Equal(FilesInPlaylistsSeeds.FipFileDInPlaylist1.IndexActual, 1);
        
        await _filesInPlaylistFacade.SortByCustomOrderAsync(playlist.Id);
        
        var fileA = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileAInPlaylist1.Id);
        
        var fileB = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileBInPlaylist1.Id);
        
        var fileC = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileCInPlaylist1.Id);
        
        var fileD = await _stofipyDbContext.FilesInPlaylists
            .SingleAsync(i => i.Id == FilesInPlaylistsSeeds.FipFileDInPlaylist1.Id);
        
        Assert.Equal(fileA.IndexActual, 1);
        Assert.Equal(fileB.IndexActual, 2);
        Assert.Equal(fileC.IndexActual, 3);
        Assert.Equal(fileD.IndexActual, 4);
    }
}