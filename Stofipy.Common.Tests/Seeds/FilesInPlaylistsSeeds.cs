using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;

namespace Stofipy.Common.Tests.Seeds;

public static class FilesInPlaylistsSeeds
{
    public static readonly FilesInPlaylistEntity FipFileAInPlaylist1 = new()
    {
        Id = Guid.Parse("1A8B6D8D-CFAD-4848-80AA-D6FAA283A750"),
        PlaylistId = PlaylistTestSeeds.Playlist1.Id,
        FileId = FileTestSeeds.FileAInPlaylist1.Id,
        Playlist = PlaylistTestSeeds.Playlist1,
        File = FileTestSeeds.FileAInPlaylist1,
        IndexActual = 2,
        IndexCustom = 1,
    };
    public static readonly FilesInPlaylistEntity FipFileBInPlaylist1 = new()
    {
        Id = Guid.Parse("1D9D94AE-F0AC-4056-9055-B9D570517C8A"),
        PlaylistId = PlaylistTestSeeds.Playlist1.Id,
        FileId = FileTestSeeds.FileBInPlaylist1.Id,
        Playlist = PlaylistTestSeeds.Playlist1,
        File = FileTestSeeds.FileBInPlaylist1,
        IndexActual = 4,
        IndexCustom = 2,
    };
    public static readonly FilesInPlaylistEntity FipFileCInPlaylist1 = new()
    {
        Id = Guid.Parse("433BE71C-33CC-4099-A849-349896FBE57B"),
        PlaylistId = PlaylistTestSeeds.Playlist1.Id,
        FileId = FileTestSeeds.FileCInPlaylist1.Id,
        Playlist = PlaylistTestSeeds.Playlist1,
        File = FileTestSeeds.FileCInPlaylist1,
        IndexActual = 3,
        IndexCustom = 3,
    };
    public static readonly FilesInPlaylistEntity FipFileDInPlaylist1 = new()
    {
        Id = Guid.Parse("E0F067CD-48D0-4D32-98B5-4B42185FF26B"),
        PlaylistId = PlaylistTestSeeds.Playlist1.Id,
        FileId = FileTestSeeds.FileDInPlaylist1.Id,
        Playlist = PlaylistTestSeeds.Playlist1,
        File = FileTestSeeds.FileDInPlaylist1,
        IndexActual = 1,
        IndexCustom = 4,
    };
    
    // 11 files in Playlist2
   public static readonly FilesInPlaylistEntity FipFile01InPlaylist2 = new()
    {
        Id = Guid.Parse("1F794072-4CFE-4030-B03D-05DB93B3EC07"),
        PlaylistId = PlaylistTestSeeds.Playlist2.Id,
        FileId = FileTestSeeds.File01.Id,
        Playlist = PlaylistTestSeeds.Playlist2,
        File = FileTestSeeds.File01,
        IndexActual = 1,
        IndexCustom = 1,
    };
    public static readonly FilesInPlaylistEntity FipFile02InPlaylist2 = new()
    {
        Id = Guid.Parse("9B8E97FF-3F6A-47BD-86C3-2305E29487BD"),
        PlaylistId = PlaylistTestSeeds.Playlist2.Id,
        FileId = FileTestSeeds.File02.Id,
        Playlist = PlaylistTestSeeds.Playlist2,
        File = FileTestSeeds.File02,
        IndexActual = 2,
        IndexCustom = 2,
    };
    public static readonly FilesInPlaylistEntity FipFile03InPlaylist2 = new()
    {
        Id = Guid.Parse("C4014308-86BF-4914-8A2F-A220F29F2944"),
        PlaylistId = PlaylistTestSeeds.Playlist2.Id,
        FileId = FileTestSeeds.File03.Id,
        Playlist = PlaylistTestSeeds.Playlist2,
        File = FileTestSeeds.File03,
        IndexActual = 3,
        IndexCustom = 3,
    };
    public static readonly FilesInPlaylistEntity FipFile04InPlaylist2 = new()
    {
        Id = Guid.Parse("66D3B32D-A94E-4AF8-A565-C933B7379229"),
        PlaylistId = PlaylistTestSeeds.Playlist2.Id,
        FileId = FileTestSeeds.File04.Id,
        Playlist = PlaylistTestSeeds.Playlist2,
        File = FileTestSeeds.File04,
        IndexActual = 4,
        IndexCustom = 4,
    };
    public static readonly FilesInPlaylistEntity FipFile05InPlaylist2 = new()
    {
        Id = Guid.Parse("8CE95DBE-EC44-47D1-9C98-D56032A56C24"),
        PlaylistId = PlaylistTestSeeds.Playlist2.Id,
        FileId = FileTestSeeds.File05.Id,
        Playlist = PlaylistTestSeeds.Playlist2,
        File = FileTestSeeds.File05,
        IndexActual = 5,
        IndexCustom = 5,
    };
    public static readonly FilesInPlaylistEntity FipFile06InPlaylist2 = new()
    {
        Id = Guid.Parse("3D8F09F4-7CA0-4B3D-9304-9B7C7C87163E"),
        PlaylistId = PlaylistTestSeeds.Playlist2.Id,
        FileId = FileTestSeeds.File06.Id,
        Playlist = PlaylistTestSeeds.Playlist2,
        File = FileTestSeeds.File06,
        IndexActual = 6,
        IndexCustom = 6,
    };
    public static readonly FilesInPlaylistEntity FipFile07InPlaylist2 = new()
    {
        Id = Guid.Parse("093E6836-8E1F-4448-A255-595A6E126E4E"),
        PlaylistId = PlaylistTestSeeds.Playlist2.Id,
        FileId = FileTestSeeds.File07.Id,
        Playlist = PlaylistTestSeeds.Playlist2,
        File = FileTestSeeds.File07,
        IndexActual = 7,
        IndexCustom = 7,
    };
    public static readonly FilesInPlaylistEntity FipFile08InPlaylist2 = new()
    {
        Id = Guid.Parse("8A3EDE67-D13A-4EBB-966D-5E40D133AE7A"),
        PlaylistId = PlaylistTestSeeds.Playlist2.Id,
        FileId = FileTestSeeds.File08.Id,
        Playlist = PlaylistTestSeeds.Playlist2,
        File = FileTestSeeds.File08,
        IndexActual = 8,
        IndexCustom = 8,
    };
    public static readonly FilesInPlaylistEntity FipFile09InPlaylist2 = new()
    {
        Id = Guid.Parse("100B3E3F-0B5A-4B49-AC33-2F468621C7C9"),
        PlaylistId = PlaylistTestSeeds.Playlist2.Id,
        FileId = FileTestSeeds.File09.Id,
        Playlist = PlaylistTestSeeds.Playlist2,
        File = FileTestSeeds.File09,
        IndexActual = 9,
        IndexCustom = 9,
    };
    public static readonly FilesInPlaylistEntity FipFile10InPlaylist2 = new()
    {
        Id = Guid.Parse("6831D6DF-EA47-4A71-AAA0-9C616BDEE8E4"),
        PlaylistId = PlaylistTestSeeds.Playlist2.Id,
        FileId = FileTestSeeds.File10.Id,
        Playlist = PlaylistTestSeeds.Playlist2,
        File = FileTestSeeds.File10,
        IndexActual = 10,
        IndexCustom = 10,
    };
    public static readonly FilesInPlaylistEntity FipFile11InPlaylist2 = new()
    {
        Id = Guid.Parse("7253A7FC-4A19-43D2-81C5-25A29C62CACD"),
        PlaylistId = PlaylistTestSeeds.Playlist2.Id,
        FileId = FileTestSeeds.File11.Id,
        Playlist = PlaylistTestSeeds.Playlist2,
        File = FileTestSeeds.File11,
        IndexActual = 11,
        IndexCustom = 11,
    };

    
    public static DbContext SeedTestFilesInPlaylists(this DbContext dbx)
    {
        dbx.Set<FilesInPlaylistEntity>().AddRange(
            FipFileAInPlaylist1 with{Playlist = null!, File = null!},
            FipFileBInPlaylist1 with{Playlist = null!, File = null!},
            FipFileCInPlaylist1 with{Playlist = null!, File = null!},
            FipFileDInPlaylist1 with{Playlist = null!, File = null!},
            
            FipFile01InPlaylist2 with{Playlist = null!, File = null!},
            FipFile02InPlaylist2 with{Playlist = null!, File = null!},
            FipFile03InPlaylist2 with{Playlist = null!, File = null!},
            FipFile04InPlaylist2 with{Playlist = null!, File = null!},
            FipFile05InPlaylist2 with{Playlist = null!, File = null!},
            FipFile06InPlaylist2 with{Playlist = null!, File = null!},
            FipFile07InPlaylist2 with{Playlist = null!, File = null!},
            FipFile08InPlaylist2 with{Playlist = null!, File = null!},
            FipFile09InPlaylist2 with{Playlist = null!, File = null!},
            FipFile10InPlaylist2 with{Playlist = null!, File = null!},
            FipFile11InPlaylist2 with{Playlist = null!, File = null!}
        );
        return dbx;
    }
}