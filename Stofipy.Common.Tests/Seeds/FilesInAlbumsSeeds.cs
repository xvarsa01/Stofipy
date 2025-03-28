using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;

namespace Stofipy.Common.Tests.Seeds;

public static class FilesInAlbumsSeeds
{
    public static FilesInAlbumEntity FileInAlbumInBasicAlbum = new()
    {
        Id = Guid.Parse("A9CFF879-C068-4EFA-A8E7-F4F552C4A3A5"),
        AlbumId = AlbumTestSeeds.BasicAlbum.Id,
        FileId = FileTestSeeds.FileInAlbum.Id,
        Album = AlbumTestSeeds.BasicAlbum,
        File = FileTestSeeds.FileInAlbum,
        Index = 1,
    };
    
    public static readonly FilesInAlbumEntity FileAInAlbumM = new()
    {
        Id = Guid.Parse("F9CD6F80-2141-4132-B9AE-5D5F6DE8CEBB"),
        AlbumId = AlbumTestSeeds.AlbumM.Id,
        FileId = FileTestSeeds.FileAInPlaylist1.Id,
        Album = AlbumTestSeeds.AlbumM,
        File = FileTestSeeds.FileAInPlaylist1,
        Index = 1,
    };
    public static readonly FilesInAlbumEntity FileBInAlbumN = new()
    {
        Id = Guid.Parse("2EEC8F8E-2497-4121-9F3F-15E65F9DED72"),
        AlbumId = AlbumTestSeeds.AlbumN.Id,
        FileId = FileTestSeeds.FileBInPlaylist1.Id,
        Album = AlbumTestSeeds.AlbumN,
        File = FileTestSeeds.FileBInPlaylist1,
        Index = 1,
    };
    public static readonly FilesInAlbumEntity FileCInAlbumO = new()
    {
        Id = Guid.Parse("D4E7A54F-C700-458A-8102-CA2253F72A7C"),
        AlbumId = AlbumTestSeeds.AlbumO.Id,
        FileId = FileTestSeeds.FileCInPlaylist1.Id,
        Album = AlbumTestSeeds.AlbumO,
        File = FileTestSeeds.FileCInPlaylist1,
        Index = 1,
    };
    public static readonly FilesInAlbumEntity FileDInAlbumP = new()
    {
        Id = Guid.Parse("C82B7B8D-D740-4723-AEFC-0A396A49C8D9"),
        AlbumId = AlbumTestSeeds.AlbumP.Id,
        FileId = FileTestSeeds.FileDInPlaylist1.Id,
        Album = AlbumTestSeeds.AlbumP,
        File = FileTestSeeds.FileDInPlaylist1,
        Index = 1,
    };
    
    public static DbContext SeedTestFilesInAlbums(this DbContext dbx)
    {
        dbx.Set<FilesInAlbumEntity>().AddRange(
            FileInAlbumInBasicAlbum with{Album = null!, File = null!},
            
            FileAInAlbumM with{Album = null!, File = null!},
            FileBInAlbumN with{Album = null!, File = null!},
            FileCInAlbumO with{Album = null!, File = null!},
            FileDInAlbumP with{Album = null!, File = null!}
        );
        return dbx;
    }
}