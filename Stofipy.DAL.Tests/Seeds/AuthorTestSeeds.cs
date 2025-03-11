using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;
using Stofipy.DAL.Seeds;

namespace Stofipy.DAL.Tests.Seeds;

public static class AuthorTestSeeds
{
    public static readonly AuthorEntity AuthorWithoutContent = new()
    {
        AuthorName = "Test Author",
        ProfilePicture = "",
        Id = Guid.Parse("4496340E-62B0-4D4F-ACAF-70416445F066"),
    };

    public static readonly AuthorEntity AuthorForFileBasic =
        AuthorWithoutContent with { Id = Guid.Parse("9AE83627-E258-4BE2-9C05-79967184A175") };

    public static readonly AuthorEntity AuthorAbc = new()
    {
        Id = Guid.Parse("420E27B9-D5DA-4F63-8B8C-DD3DE300D021"),
        AuthorName = "ABC",
        ProfilePicture = null,
        Albums = []
    };
    
    // seeds for Album tests
    public static readonly AuthorEntity AuthorWithEmptyAlbum = new()
    {
        Id = Guid.Parse("F8B4CDAA-F545-4573-9E78-1B63D976124F"),
        AuthorName = "EmptyAlbum",
        ProfilePicture = null,
        Albums = []
    };
    public static readonly AuthorEntity AuthorForAlbumUpdateAndDelete = new()
    {
        Id = Guid.Parse("FFFE8F8B-881A-4A31-93B2-0A7902DDA4EF"),
        AuthorName = "Album Update",
    };

    static AuthorTestSeeds()
    {
        AuthorForFileBasic.Albums.Add(AlbumTestSeeds.BasicAlbum);
        AuthorWithEmptyAlbum.Albums.Add(AlbumTestSeeds.EmptyAlbum);
        AuthorForAlbumUpdateAndDelete.Albums.Add(AlbumTestSeeds.AlbumForUpdate);
        AuthorForAlbumUpdateAndDelete.Albums.Add(AlbumTestSeeds.AlbumForDelete);
        
        AuthorForFileBasic.Files.Add(FileTestSeeds.FileBasic);
        AuthorForFileBasic.Files.Add(FileTestSeeds.FileInPlaylist1);
        AuthorForFileBasic.Files.Add(FileTestSeeds.FileInAlbum);
        AuthorForFileBasic.Files.Add(FileTestSeeds.FileForUpdate);
        AuthorForFileBasic.Files.Add(FileTestSeeds.FileForDelete);
        AuthorForFileBasic.Files.Add(FileTestSeeds.FileInPlaylist2);
        
        AuthorAbc.Files.Add(FileTestSeeds.File1OfAuthorAbc);
        AuthorAbc.Files.Add(FileTestSeeds.File2OfAuthorAbc);
    }
    
    public static DbContext SeedTestAuthors(this DbContext dbx)
    {
        dbx.Set<AuthorEntity>().AddRange(
            AuthorWithoutContent with{Albums = [], Files = []},
            AuthorForFileBasic with{Albums = [], Files = []},
            AuthorAbc with{Albums = [], Files = []},
            
            AuthorWithEmptyAlbum with{Albums = [], Files = []},
            AuthorForAlbumUpdateAndDelete with{Albums = [], Files = []}
        );
        return dbx;
    }
}