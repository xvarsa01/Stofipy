using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;

namespace Stofipy.Common.Tests.Seeds;

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
    
    public static readonly AuthorEntity AuthorU = new()
    {
        Id = Guid.Parse("26C0D7C4-F009-4AB5-A667-189D95F5356D"),
        AuthorName = "Author U",
    };    
    public static readonly AuthorEntity AuthorV = new()
    {
        Id = Guid.Parse("BAAC24D3-EC3B-40CB-A62E-098DF44F73A9"),
        AuthorName = "Author V",
    };    
    public static readonly AuthorEntity AuthorX = new()
    {
        Id = Guid.Parse("76B3A7D4-359A-4EE1-ABC3-FC2F96BF8EC3"),
        AuthorName = "Author X",
    };    
    public static readonly AuthorEntity AuthorY = new()
    {
        Id = Guid.Parse("2997EC19-9B87-44A1-8690-D5C482407F7E"),
        AuthorName = "Author Y",
    };

    static AuthorTestSeeds()
    {
        AuthorForFileBasic.Albums.Add(AlbumTestSeeds.BasicAlbum);
        AuthorWithEmptyAlbum.Albums.Add(AlbumTestSeeds.EmptyAlbum);
        AuthorForAlbumUpdateAndDelete.Albums.Add(AlbumTestSeeds.AlbumForUpdate);
        AuthorForAlbumUpdateAndDelete.Albums.Add(AlbumTestSeeds.AlbumForDelete);
        
        AuthorU.Albums.Add(AlbumTestSeeds.AlbumM);
        AuthorV.Albums.Add(AlbumTestSeeds.AlbumN);
        AuthorX.Albums.Add(AlbumTestSeeds.AlbumO);
        AuthorY.Albums.Add(AlbumTestSeeds.AlbumP);
        
        AuthorForFileBasic.Files.Add(FileTestSeeds.FileBasic);
        AuthorForFileBasic.Files.Add(FileTestSeeds.FileInAlbum1);
        AuthorForFileBasic.Files.Add(FileTestSeeds.FileForUpdate);
        AuthorForFileBasic.Files.Add(FileTestSeeds.FileForDelete);
        AuthorU.Files.Add(FileTestSeeds.FileAInPlaylist1);
        AuthorV.Files.Add(FileTestSeeds.FileBInPlaylist1);
        AuthorX.Files.Add(FileTestSeeds.FileCInPlaylist1);
        AuthorY.Files.Add(FileTestSeeds.FileDInPlaylist1);
        
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
            AuthorForAlbumUpdateAndDelete with{Albums = [], Files = []},
            
            AuthorU with{Albums = [], Files = []},
            AuthorV with{Albums = [], Files = []},
            AuthorX with{Albums = [], Files = []},
            AuthorY with{Albums = [], Files = []}
        );
        return dbx;
    }
}