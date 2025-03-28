using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;

namespace Stofipy.Common.Tests.Seeds;

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

    
    public static AlbumEntity AlbumM = new()
    {
        Id = Guid.Parse("5CF3F55E-86E2-408B-AE7C-829A28337C39"),
        AlbumName = "Album M",
        Description = "",
        AuthorId = AuthorTestSeeds.AuthorU.Id,
        Author = AuthorTestSeeds.AuthorU,
    };
    public static AlbumEntity AlbumN = new()
    {
        Id = Guid.Parse("D995E480-4F14-4D52-AFC0-328FC07CFBEB"),
        AlbumName = "Album N",
        Description = "",
        AuthorId = AuthorTestSeeds.AuthorV.Id,
        Author = AuthorTestSeeds.AuthorV,
    };
    public static AlbumEntity AlbumO = new()
    {
        Id = Guid.Parse("35800BC6-4214-4C46-841C-E6FDC28EB1BA"),
        AlbumName = "Album O",
        Description = "",
        AuthorId = AuthorTestSeeds.AuthorX.Id,
        Author = AuthorTestSeeds.AuthorX,
    };
    public static AlbumEntity AlbumP = new()
    {
        Id = Guid.Parse("1C8B19CA-B4DB-4F81-AEA8-6281568AEF6F"),
        AlbumName = "Album P",
        Description = "",
        AuthorId = AuthorTestSeeds.AuthorY.Id,
        Author = AuthorTestSeeds.AuthorY,
    };
    static AlbumTestSeeds()
    {
        BasicAlbum.FilesInAlbums.Add(FilesInAlbumsSeeds.FileInAlbumInBasicAlbum);
        
        AlbumM.FilesInAlbums.Add(FilesInAlbumsSeeds.FileInAlbumInBasicAlbum);
        AlbumN.FilesInAlbums.Add(FilesInAlbumsSeeds.FileInAlbumInBasicAlbum);
        AlbumO.FilesInAlbums.Add(FilesInAlbumsSeeds.FileInAlbumInBasicAlbum);
        AlbumP.FilesInAlbums.Add(FilesInAlbumsSeeds.FileInAlbumInBasicAlbum);    }

    public static DbContext SeedTestAlbums(this DbContext dbx)
    {
        dbx.Set<AlbumEntity>().AddRange(
            BasicAlbum with{FilesInAlbums = [], Author = null!},
            EmptyAlbum with{FilesInAlbums = [], Author = null!},
            AlbumForUpdate with{FilesInAlbums = [], Author = null!},
            AlbumForDelete with{FilesInAlbums = [], Author = null!},
            
            AlbumM with{FilesInAlbums = [], Author = null!},
            AlbumN with{FilesInAlbums = [], Author = null!},
            AlbumO with{FilesInAlbums = [], Author = null!},
            AlbumP with{FilesInAlbums = [], Author = null!}
        );
        return dbx;
    }
}