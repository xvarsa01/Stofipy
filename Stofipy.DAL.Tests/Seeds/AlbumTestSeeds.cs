using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;
using Stofipy.DAL.Seeds;

namespace Stofipy.DAL.Tests.Seeds;

public static class AlbumTestSeeds
{
    public static AlbumEntity BasicAlbum = new()
    {
        Id = Guid.Parse("4AFD3389-1C14-4084-9C18-A055C9AF1009"),
        AlbumName = "Basic Album",
        Description = "",
        Author = AuthorTestSeeds.AuthorForFileBasic,
        AuthorId = AuthorTestSeeds.AuthorForFileBasic.Id,
    };

    public static AlbumEntity EmptyAlbum = new()
    {
        Id = Guid.Parse("2B55DAD3-2B9F-4EF2-B08A-B1740009A7D2"),
        AlbumName = "empty Album",
        Description = "empty Album",
        AuthorId = AuthorTestSeeds.AuthorWithEmptyAlbum.Id,
        Author = AuthorTestSeeds.AuthorWithEmptyAlbum,
        FilesInAlbums = []
    };

    public static AlbumEntity AlbumForUpdate = new()
    {
        Id = Guid.Parse("6086146E-90DF-4F83-B58F-FF0AECC7BBA5"),
        AlbumName = "Album for update",
        Description = "",
        AuthorId = AuthorTestSeeds.AuthorForAlbumUpdateAndDelete.Id,
        Author = AuthorTestSeeds.AuthorForAlbumUpdateAndDelete,
    };
    public static AlbumEntity AlbumForDelete = new()
    {
        Id = Guid.Parse("1E257C7B-9E0D-47B2-BB22-F1EE46AB9E25"),
        AlbumName = "Album for delete",
        Description = "",
        AuthorId = AuthorTestSeeds.AuthorForAlbumUpdateAndDelete.Id,
        Author = AuthorTestSeeds.AuthorForAlbumUpdateAndDelete,
    };

    static AlbumTestSeeds()
    {
        BasicAlbum.FilesInAlbums.Add(FilesInAlbumsSeeds.FileInAlbumInBasicAlbum);
    }

    public static DbContext SeedTestAlbums(this DbContext dbx)
    {
        dbx.Set<AlbumEntity>().AddRange(
            BasicAlbum with{FilesInAlbums = [], Author = null!},
            EmptyAlbum with{FilesInAlbums = [], Author = null!},
            AlbumForUpdate with{FilesInAlbums = [], Author = null!},
            AlbumForDelete with{FilesInAlbums = [], Author = null!}
        );
        return dbx;
    }
}