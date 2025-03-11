using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;
using Stofipy.DAL.Enums;
using Stofipy.DAL.Seeds;

namespace Stofipy.DAL.Tests.Seeds;

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
        Category = Category.Rock,
        Author = AuthorTestSeeds.AuthorForFileBasic,
        AuthorId = AuthorTestSeeds.AuthorForFileBasic.Id,
        FilesInAlbums = [],
        FilesInPlaylists = [],
    };

    public static readonly FileEntity FileInPlaylist1 = new()
    {
        Id = Guid.Parse("686D9C06-FCE8-4F0A-A296-34E78AB320D8"),
        FileName = "File In Playlist 1",
        Description = "File In Playlist 1 description",
        Size = 2.5,
        Length = 188,
        Category = Category.Rock,
        Author = AuthorTestSeeds.AuthorForFileBasic,
        AuthorId = AuthorTestSeeds.AuthorForFileBasic.Id,
        FilesInAlbums = [],
        FilesInPlaylists = [],
    };
    public static readonly FileEntity FileInAlbum = FileBasic with { Id = Guid.Parse("10BDAF90-C5A7-4CA0-87B0-9E72C3DCF13E"),FilesInAlbums = [], FilesInPlaylists = []};
    public static readonly FileEntity FileForUpdate = FileBasic with { Id = Guid.Parse("9602D4B3-37EA-476C-811A-49E1B2FAEA87"),FilesInAlbums = [], FilesInPlaylists = [] };
    public static readonly FileEntity FileForDelete = FileBasic with { Id = Guid.Parse("B0DFD184-B2B8-4904-96F7-72FF94F36797"),FilesInAlbums = [], FilesInPlaylists = [] };
    public static readonly FileEntity FileInPlaylist2 = FileInPlaylist1 with { Id = Guid.Parse("A2387795-75B6-4784-8742-EC06F88D6E54"),FilesInAlbums = [], FilesInPlaylists = [] };
    
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
    
    static FileTestSeeds()
    {
        FileInAlbum.FilesInAlbums.Add(FilesInAlbumsSeeds.FileInAlbumInBasicAlbum);
    }
    public static DbContext SeedTestFiles(this DbContext dbx)
    {
        dbx.Set<FileEntity>().AddRange(
            FileBasic with{Author = null!, FilesInAlbums = [], FilesInPlaylists = []},
            FileInAlbum with{Author = null!, FilesInAlbums = [], FilesInPlaylists = []},
            FileForUpdate with{Author = null!, FilesInAlbums = [], FilesInPlaylists = []},
            FileForDelete with{Author = null!, FilesInAlbums = [], FilesInPlaylists = []},
            FileInPlaylist1 with{Author = null!, FilesInAlbums = [], FilesInPlaylists = []},
            FileInPlaylist2 with{Author = null!, FilesInAlbums = [], FilesInPlaylists = []},
            File1OfAuthorAbc with{Author = null!, FilesInAlbums = [], FilesInPlaylists = []},
            File2OfAuthorAbc with{Author = null!, FilesInAlbums = [], FilesInPlaylists = []}
        );
        return dbx;
    }
}