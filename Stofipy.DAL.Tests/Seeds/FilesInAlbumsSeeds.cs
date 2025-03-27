using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;

namespace Stofipy.DAL.Tests.Seeds;

public static class FilesInAlbumsSeeds
{
    public static FilesInAlbumEntity FileInAlbumInBasicAlbum = new()
    {
        Id = Guid.Parse("A9CFF879-C068-4EFA-A8E7-F4F552C4A3A5"),
        AlbumId = AlbumTestSeeds.BasicAlbum.Id,
        FileId = FileTestSeeds.FileInAlbum.Id,
        Album = AlbumTestSeeds.BasicAlbum,
        File = FileTestSeeds.FileInAlbum,
        Index = 0,
    };
    
    public static DbContext SeedTestFilesInAlbums(this DbContext dbx)
    {
        dbx.Set<FilesInAlbumEntity>().AddRange(
            FileInAlbumInBasicAlbum with{Album = null!, File = null!}
        );
        return dbx;
    }
}