using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;

namespace Stofipy.Common.Tests.Seeds;

public static class FilesInAlbumsSeeds
{
    public static FilesInAlbumEntity FileInAlbum1InBasicAlbum = new()
    {
        Id = Guid.Parse("A9CFF879-C068-4EFA-A8E7-F4F552C4A3A5"),
        AlbumId = AlbumTestSeeds.BasicAlbum.Id,
        FileId = FileTestSeeds.FileInAlbum1.Id,
        Album = AlbumTestSeeds.BasicAlbum,
        File = FileTestSeeds.FileInAlbum1,
        Index = 1,
    };
    public static FilesInAlbumEntity FileInAlbum2InBasicAlbum = new()
    {
        Id = Guid.Parse("E73ECB87-204B-4734-AE5E-6F8010CA9F70"),
        AlbumId = AlbumTestSeeds.BasicAlbum.Id,
        FileId = FileTestSeeds.FileInAlbum2.Id,
        Album = AlbumTestSeeds.BasicAlbum,
        File = FileTestSeeds.FileInAlbum2,
        Index = 2,
    };
    public static FilesInAlbumEntity FileInAlbum3InBasicAlbum = new()
    {
        Id = Guid.Parse("2F069120-7D9A-4335-8D25-DC6E2F84A7A9"),
        AlbumId = AlbumTestSeeds.BasicAlbum.Id,
        FileId = FileTestSeeds.FileInAlbum3.Id,
        Album = AlbumTestSeeds.BasicAlbum,
        File = FileTestSeeds.FileInAlbum3,
        Index = 3,
    };
    public static FilesInAlbumEntity FileInAlbum4InBasicAlbum = new()
    {
        Id = Guid.Parse("F437CF1C-0595-455B-A3FD-FC4717F8808A"),
        AlbumId = AlbumTestSeeds.BasicAlbum.Id,
        FileId = FileTestSeeds.FileInAlbum4.Id,
        Album = AlbumTestSeeds.BasicAlbum,
        File = FileTestSeeds.FileInAlbum4,
        Index = 4,
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

    public static readonly FilesInAlbumEntity FileInAlbumForDelete = new()
    {
        Id = Guid.Parse("BD924A55-4C8F-4077-8D0A-101854277B04"),
        AlbumId = AlbumTestSeeds.AlbumForDelete.Id,
        FileId = FileTestSeeds.FileForDeletedAlbum.Id,
        Album = AlbumTestSeeds.AlbumForDelete,
        File = FileTestSeeds.FileForDeletedAlbum,
        Index = 0
    };

    public static DbContext SeedTestFilesInAlbums(this DbContext dbx)
    {
        dbx.Set<FilesInAlbumEntity>().AddRange(
            FileInAlbum1InBasicAlbum with{Album = null!, File = null!},
            FileInAlbum2InBasicAlbum with{Album = null!, File = null!},
            FileInAlbum3InBasicAlbum with{Album = null!, File = null!},
            FileInAlbum4InBasicAlbum with{Album = null!, File = null!},
            
            FileAInAlbumM with{Album = null!, File = null!},
            FileBInAlbumN with{Album = null!, File = null!},
            FileCInAlbumO with{Album = null!, File = null!},
            FileDInAlbumP with{Album = null!, File = null!},
            
            FileInAlbumForDelete with{Album = null!, File = null!}
        );
        return dbx;
    }
}