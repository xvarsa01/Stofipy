using Microsoft.EntityFrameworkCore;
using Stofipy.Common.Tests;
using Stofipy.Common.Tests.Seeds;
using Stofipy.DAL.Entities;
using Stofipy.DAL.Enums;
using Xunit.Abstractions;

namespace Stofipy.DAL.Tests;

public class PlaylistTests  (ITestOutputHelper output): DbContextTestsBase(output)
{
    [Fact]
    public async Task AddNewEmpty_Playlist_Persisted()
    {
        //Arrange
        PlaylistEntity playlistEntity = new()
        {
            Id = Guid.Parse("910B9FB2-D88F-40E8-BFDD-87106D9DBFBD"),
            PlaylistName = "New Playlist",
            Description = "New Description",
        };

        //Act
        DbContextSUT.Playlists.Add(playlistEntity);
        await DbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntities = await dbx.Playlists
            .SingleAsync(i => i.Id == playlistEntity.Id);
        DeepAssert.Equal(playlistEntity, actualEntities);
    }

    [Fact]
    public async Task AddNew_PlaylistWithNewFile_Persisted()
    {
        //Arrange
        FileEntity fileEntity = new()
        {
            Id = Guid.Parse("D531CEE4-59E2-4948-9F76-0368019450BB"),
            FileName = "New File",
            Description = "New Description",
            Size = 0,
            Length = 0,
            Category = Category.Rock,
            AuthorId = AuthorTestSeeds.AuthorForFileBasic.Id,
            Author = AuthorTestSeeds.AuthorForFileBasic,
            FilesInPlaylists = [],
            FilesInAlbums = [],
        };
        PlaylistEntity playlistEntity = new()
        {
            Id = Guid.Parse("769A8649-3DB8-4FE8-89CC-EA36F3DB7DB6"),
            PlaylistName = "New Playlist",
            Description = "New Description",
            FilesInPlaylists = []
        };
        var fileInPlaylist = new FilesInPlaylistEntity
        {
            Id = Guid.Parse("A9C4183B-6396-499A-8CCF-52806A271657"),
            PlaylistId = playlistEntity.Id,
            FileId = fileEntity.Id,
            Playlist = playlistEntity,
            File = fileEntity,
            IndexActual = 0,
            IndexCustom = 0
        };
        playlistEntity.FilesInPlaylists.Add(fileInPlaylist);
        
        //Act
        await Assert.ThrowsAsync<ArgumentNullException>(async () =>
        {
            DbContextSUT.Playlists.Add(playlistEntity);
            await DbContextSUT.SaveChangesAsync();
        });
    }
}