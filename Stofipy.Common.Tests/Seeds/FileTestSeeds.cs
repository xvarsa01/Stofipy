using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;
using Stofipy.DAL.Enums;

namespace Stofipy.Common.Tests.Seeds;

public static class FileTestSeeds
{
    public static readonly FileEntity FileBasic = new()
    {
        Id = Guid.Parse("8B4BA200-AF18-4A9A-AE9C-91D54602D715"),
        FileName = "File 1",
        Description = "File 1 description",
        Lyrics = null,
        Size = 2.5,
        Length = 188,
        PlayCount = 120120,
        Category = Category.Rock,
        Author = AuthorTestSeeds.AuthorForFileBasic,
        AuthorId = AuthorTestSeeds.AuthorForFileBasic.Id,
        FilesInAlbums = [],
        FilesInPlaylists = [],
    };
    
    public static readonly FileEntity File1OfAuthorAbc = FileBasic with
    {
        Id = Guid.Parse("60595298-C2FD-4621-A36D-F32218B98532"),
        Author = AuthorTestSeeds.AuthorAbc,
        AuthorId = AuthorTestSeeds.AuthorAbc.Id
    };
    public static readonly FileEntity File2OfAuthorAbc = FileBasic with 
    {
        Id = Guid.Parse("1FF32AFF-533B-4FB1-9649-A03A416D216C"),
        Author = AuthorTestSeeds.AuthorAbc,
        AuthorId = AuthorTestSeeds.AuthorAbc.Id
    };

    public static readonly FileEntity FileInAlbum1 = FileBasic with { Id = Guid.Parse("10BDAF90-C5A7-4CA0-87B0-9E72C3DCF13E"),FilesInAlbums = [], FilesInPlaylists = []};
    public static readonly FileEntity FileInAlbum2 = FileBasic with { Id = Guid.Parse("D5002144-FCDC-48C3-85EB-683602599919"),FilesInAlbums = [], FilesInPlaylists = []};
    public static readonly FileEntity FileInAlbum3 = FileBasic with { Id = Guid.Parse("F23F2B20-5745-4B5C-91E9-321CCA72C8C9"),FilesInAlbums = [], FilesInPlaylists = []};
    public static readonly FileEntity FileInAlbum4 = FileBasic with { Id = Guid.Parse("A32EEBCF-A382-48B8-94C1-6805BC2AC7AA"),FilesInAlbums = [], FilesInPlaylists = []};
    public static readonly FileEntity FileForUpdate = FileBasic with { Id = Guid.Parse("9602D4B3-37EA-476C-811A-49E1B2FAEA87"),FilesInAlbums = [], FilesInPlaylists = [] };
    public static readonly FileEntity FileForDelete = FileBasic with { Id = Guid.Parse("B0DFD184-B2B8-4904-96F7-72FF94F36797"),FilesInAlbums = [], FilesInPlaylists = [] };
    public static readonly FileEntity FileForDeletedAlbum = FileBasic with { Id = Guid.Parse("EECF6DC6-4284-4E3E-BD4A-21F250A934FE"),FilesInAlbums = [], FilesInPlaylists = [] };

    public static readonly FileEntity FileDInPlaylist1 = new()
    {
        Id = Guid.Parse("686D9C06-FCE8-4F0A-A296-34E78AB320D8"),
        FileName = "File D",
        Description = "File D In Playlist 1 description",
        Size = 2.5,
        Length = 400,
        PlayCount = 0,
        Category = Category.Rock,
        Author = AuthorTestSeeds.AuthorY,
        AuthorId = AuthorTestSeeds.AuthorY.Id,
        FilesInAlbums = [],
        FilesInPlaylists = [],
        DefaultAlbum = AlbumTestSeeds.AlbumP,
        DefaultAlbumId = AlbumTestSeeds.AlbumP.Id

    };
    public static readonly FileEntity FileAInPlaylist1 = FileDInPlaylist1 with
    {
        Id = Guid.Parse("A2387795-75B6-4784-8742-EC06F88D6E54"), FileName = "File A", Length = 100,
        Author = AuthorTestSeeds.AuthorU, AuthorId = AuthorTestSeeds.AuthorU.Id, FilesInAlbums = [], FilesInPlaylists = [],
        DefaultAlbum = AlbumTestSeeds.AlbumM, DefaultAlbumId = AlbumTestSeeds.AlbumM.Id
    };

    public static readonly FileEntity FileCInPlaylist1 = FileDInPlaylist1 with
    {
        Id = Guid.Parse("B8B38EF9-BFDF-4222-B660-F43AF0D68385"), FileName = "File C", Length = 300,
        Author = AuthorTestSeeds.AuthorX, AuthorId = AuthorTestSeeds.AuthorX.Id, FilesInAlbums = [], FilesInPlaylists = [],
        DefaultAlbum = AlbumTestSeeds.AlbumO, DefaultAlbumId = AlbumTestSeeds.AlbumO.Id
    };

    public static readonly FileEntity FileBInPlaylist1 = FileDInPlaylist1 with
    {
        Id = Guid.Parse("09A9C78D-90B1-4FFD-BC8C-2CEAB15780C1"), FileName = "File B", Length = 200,
        Author = AuthorTestSeeds.AuthorV, AuthorId = AuthorTestSeeds.AuthorV.Id, FilesInAlbums = [], FilesInPlaylists = [],
        DefaultAlbum = AlbumTestSeeds.AlbumN, DefaultAlbumId = AlbumTestSeeds.AlbumN.Id
    };

    public static readonly FileEntity File01 = new()
    {
        Id = Guid.Parse("58DD50D8-29C3-4999-A39C-CF97F6D9A1B7"),
        FileName = "File01",
        Description = "",
        Lyrics = null,
        Size = 2.5,
        Length = 123,
        PlayCount = 0,
        Category = Category.Rock,
        Author = AuthorTestSeeds.AuthorForFileBasic,
        AuthorId = AuthorTestSeeds.AuthorForFileBasic.Id,
        FilesInAlbums = [],
        FilesInPlaylists = [],
    };
    public static readonly FileEntity File02 = File01 with{Id = Guid.Parse("A1B2C3D4-1234-5678-9ABC-DEF012345678"), FileName = "File02"};
    public static readonly FileEntity File03 = File01 with{Id = Guid.Parse("B2C3D4E5-2345-6789-ABCD-EF1234567890"), FileName = "File03"};
    public static readonly FileEntity File04 = File01 with{Id = Guid.Parse("C3D4E5F6-3456-789A-BCDE-F23456789012"), FileName = "File04"};
    public static readonly FileEntity File05 = File01 with{Id = Guid.Parse("931319CA-D24D-438C-92B4-623229C7DCC9"), FileName = "File05"};
    public static readonly FileEntity File06 = File01 with{Id = Guid.Parse("88FFE4EC-E84A-4F27-BA5A-A677388D7DC6"), FileName = "File06"};
    public static readonly FileEntity File07 = File01 with{Id = Guid.Parse("16B2E140-7C8E-4D2D-BF4B-2DA70C4C2B99"), FileName = "File07"};
    public static readonly FileEntity File08 = File01 with{Id = Guid.Parse("C206AFD2-9F30-4AB8-8E73-0AF94B15BFFE"), FileName = "File08"};
    public static readonly FileEntity File09 = File01 with{Id = Guid.Parse("A9EF3C4D-B603-474A-A692-5C238D6D78B8"), FileName = "File09"};
    public static readonly FileEntity File10 = File01 with{Id = Guid.Parse("04061510-13DF-47A9-B13B-C7B8BB7A6682"), FileName = "File10"};
    public static readonly FileEntity File11 = File01 with{Id = Guid.Parse("9558BB05-FBF9-416D-A582-BD0A21EFAB80"), FileName = "File11"};


    static FileTestSeeds()
    {
        FileInAlbum1.FilesInAlbums.Add(FilesInAlbumsSeeds.FileInAlbum1InBasicAlbum);
        FileForDeletedAlbum.FilesInAlbums.Add(FilesInAlbumsSeeds.FileInAlbumForDelete);
    }
    public static DbContext SeedTestFiles(this DbContext dbx)
    {
        dbx.Set<FileEntity>().AddRange(
            FileBasic with{Author = null!, FilesInAlbums = [], FilesInPlaylists = []},
            FileInAlbum1 with{Author = null!, FilesInAlbums = [], FilesInPlaylists = []},
            FileInAlbum2 with{Author = null!, FilesInAlbums = [], FilesInPlaylists = []},
            FileInAlbum3 with{Author = null!, FilesInAlbums = [], FilesInPlaylists = []},
            FileInAlbum4 with{Author = null!, FilesInAlbums = [], FilesInPlaylists = []},
            FileForUpdate with{Author = null!, FilesInAlbums = [], FilesInPlaylists = []},
            FileForDelete with{Author = null!, FilesInAlbums = [], FilesInPlaylists = []},
            FileForDeletedAlbum with{Author = null!, FilesInAlbums = [], FilesInPlaylists = []},
            FileAInPlaylist1 with{Author = null!, FilesInAlbums = [], FilesInPlaylists = [], DefaultAlbum = null!},
            FileBInPlaylist1 with{Author = null!, FilesInAlbums = [], FilesInPlaylists = [], DefaultAlbum = null!},
            FileCInPlaylist1 with{Author = null!, FilesInAlbums = [], FilesInPlaylists = [], DefaultAlbum = null!},
            FileDInPlaylist1 with{Author = null!, FilesInAlbums = [], FilesInPlaylists = [], DefaultAlbum = null!},
            File1OfAuthorAbc with{Author = null!, FilesInAlbums = [], FilesInPlaylists = []},
            File2OfAuthorAbc with{Author = null!, FilesInAlbums = [], FilesInPlaylists = []},
            
            File01 with{Author = null!, FilesInAlbums = [], FilesInPlaylists = []},
            File02 with{Author = null!, FilesInAlbums = [], FilesInPlaylists = []},
            File03 with{Author = null!, FilesInAlbums = [], FilesInPlaylists = []},
            File04 with{Author = null!, FilesInAlbums = [], FilesInPlaylists = []},
            File05 with{Author = null!, FilesInAlbums = [], FilesInPlaylists = []},
            File06 with{Author = null!, FilesInAlbums = [], FilesInPlaylists = []},
            File07 with{Author = null!, FilesInAlbums = [], FilesInPlaylists = []},
            File08 with{Author = null!, FilesInAlbums = [], FilesInPlaylists = []},
            File09 with{Author = null!, FilesInAlbums = [], FilesInPlaylists = []},
            File10 with{Author = null!, FilesInAlbums = [], FilesInPlaylists = []},
            File11 with{Author = null!, FilesInAlbums = [], FilesInPlaylists = []}
        );
        return dbx;
    }
}