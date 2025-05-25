using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;

namespace Stofipy.DAL.Seeds;

public static class FilesInPlaylistSeeds
{
    public static FilesInPlaylistEntity MyFavouritesFile1 = new()
    {
        Id = Guid.Parse("FB4E55D8-5A81-4E99-9D19-BB27EA9C7377"),
        PlaylistId = PlaylistSeeds.MyFavourites.Id,
        FileId = FileSeeds.Vlak.Id,
        Playlist = PlaylistSeeds.MyFavourites,
        File = FileSeeds.Vlak,
        IndexActual = 1,
        IndexCustom = 1
    };
    
    public static FilesInPlaylistEntity MyFavouritesFile2 = new()
    {
        Id = Guid.Parse("AC9625EE-3773-4F2C-A07D-E77D04CC4864"),
        PlaylistId = PlaylistSeeds.MyFavourites.Id,
        FileId = FileSeeds.JedenDenVKyjeve.Id,
        Playlist = PlaylistSeeds.MyFavourites,
        File = FileSeeds.JedenDenVKyjeve,
        IndexActual = 2,
        IndexCustom = 2
    };
    
    public static FilesInPlaylistEntity MyFavouritesFile3 = new()
    {
        Id = Guid.Parse("53714273-FB31-49ED-9AB7-6D525E8E108A"),
        PlaylistId = PlaylistSeeds.MyFavourites.Id,
        FileId = FileSeeds.Abstinent.Id,
        Playlist = PlaylistSeeds.MyFavourites,
        File = FileSeeds.Abstinent,
        IndexActual = 3,
        IndexCustom = 3
    };
    
    public static FilesInPlaylistEntity MyFavouritesFile4 = new()
    {
        Id = Guid.Parse("9AF05FB9-F78D-495D-8413-D341DEBB3790"),
        PlaylistId = PlaylistSeeds.MyFavourites.Id,
        FileId = FileSeeds.SilnyRefren.Id,
        Playlist = PlaylistSeeds.MyFavourites,
        File = FileSeeds.SilnyRefren,
        IndexActual = 4,
        IndexCustom = 4
    };
    
    public static FilesInPlaylistEntity MyFavouritesFile5 = new()
    {
        Id = Guid.Parse("BD8CA7CD-E3C4-48AA-A6CD-F3BD8E03DF01"),
        PlaylistId = PlaylistSeeds.MyFavourites.Id,
        FileId = FileSeeds.PreTychCoOstali.Id,
        Playlist = PlaylistSeeds.MyFavourites,
        File = FileSeeds.PreTychCoOstali,
        IndexActual = 5,
        IndexCustom = 5
    };
    
    public static DbContext SeedFilesInPlaylists(this DbContext dbx)
    {
        dbx.Set<FilesInPlaylistEntity>().AddRange(
            MyFavouritesFile1 with{File = null!, Playlist = null!},
            MyFavouritesFile2 with{File = null!, Playlist = null!},
            MyFavouritesFile3 with{File = null!, Playlist = null!},
            MyFavouritesFile4 with{File = null!, Playlist = null!},
            MyFavouritesFile5 with{File = null!, Playlist = null!}
        );
        return dbx;
    }
}