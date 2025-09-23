using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;

namespace Stofipy.Common.Tests.Seeds;

public static class PlaylistTestSeeds
{
    public static readonly PlaylistEntity Playlist1 = new()
    {
        Id = Guid.Parse("75B680D4-5580-4990-86C1-04B301C3F9D7"),
        PlaylistName = "Playlist with 4 files",
        Description = "",
        PlayCount = 0,
        IsPublic = false,
        FilesInPlaylists = [],
        CreatedBy = ProfileTestSeeds.User1,
        CreatedById = ProfileTestSeeds.User1.Id,
    };
    
    public static readonly PlaylistEntity Playlist2 = new()
    {
        Id = Guid.Parse("D6E065CC-0572-457B-9500-FDE1C7DB4E98"),
        PlaylistName = "Playlist with 11 files",
        Description = "11 files",
        PlayCount = 0,
        IsPublic = false,
        FilesInPlaylists = [],
        CreatedBy = ProfileTestSeeds.User1,
        CreatedById = ProfileTestSeeds.User1.Id,
    };
    

    static PlaylistTestSeeds()
    {
        Playlist1.FilesInPlaylists.Add(FilesInPlaylistsSeeds.FipFileAInPlaylist1);
        Playlist1.FilesInPlaylists.Add(FilesInPlaylistsSeeds.FipFileBInPlaylist1);
        Playlist1.FilesInPlaylists.Add(FilesInPlaylistsSeeds.FipFileCInPlaylist1);
        Playlist1.FilesInPlaylists.Add(FilesInPlaylistsSeeds.FipFileDInPlaylist1);
        
        Playlist2.FilesInPlaylists.Add(FilesInPlaylistsSeeds.FipFile01InPlaylist2);
        Playlist2.FilesInPlaylists.Add(FilesInPlaylistsSeeds.FipFile02InPlaylist2);
        Playlist2.FilesInPlaylists.Add(FilesInPlaylistsSeeds.FipFile03InPlaylist2);
        Playlist2.FilesInPlaylists.Add(FilesInPlaylistsSeeds.FipFile04InPlaylist2);
        Playlist2.FilesInPlaylists.Add(FilesInPlaylistsSeeds.FipFile05InPlaylist2);
        Playlist2.FilesInPlaylists.Add(FilesInPlaylistsSeeds.FipFile06InPlaylist2);
        Playlist2.FilesInPlaylists.Add(FilesInPlaylistsSeeds.FipFile07InPlaylist2);
        Playlist2.FilesInPlaylists.Add(FilesInPlaylistsSeeds.FipFile08InPlaylist2);
        Playlist2.FilesInPlaylists.Add(FilesInPlaylistsSeeds.FipFile09InPlaylist2);
        Playlist2.FilesInPlaylists.Add(FilesInPlaylistsSeeds.FipFile10InPlaylist2);
        Playlist2.FilesInPlaylists.Add(FilesInPlaylistsSeeds.FipFile11InPlaylist2);
    }

    public static DbContext SeedTestPlaylists(this DbContext dbx)
    {
        dbx.Set<PlaylistEntity>().AddRange(
            Playlist1 with{FilesInPlaylists = [], CreatedBy = null!},
            Playlist2 with{FilesInPlaylists = [], CreatedBy = null!}
        );
        return dbx;
    }
}