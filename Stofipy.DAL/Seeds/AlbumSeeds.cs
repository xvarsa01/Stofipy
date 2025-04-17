using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Stofipy.DAL.Entities;

namespace Stofipy.DAL.Seeds;

public static class AlbumSeeds
{
    public static AlbumEntity ParaPreVsetkoOKoloNas = new()
    {
        Id = Guid.Parse("3C119654-F637-4DA9-9CB9-09DC14156FC2"),
        AlbumName = "Pre Všetko Okolo Nás",
        Description = "",
        Picture = "Resources\\Images\\Albums\\para_nasou_krajinou.jpg",
        Year = 2024,
        AuthorId = AuthorSeeds.Para.Id,
        Author = AuthorSeeds.Para,
        FilesInAlbums = []
    };
    
    public static AlbumEntity ParaNasouKrajinou = new()
    {
        Id = Guid.Parse("6DE68F45-72BF-41D7-9D9D-BA828381A797"),
        AlbumName = "Našou Krajinou",
        Description = "",
        Picture = "Resources\\Images\\Albums\\para_nasou_krajinou.jpg",
        Year = 2018,
        AuthorId = AuthorSeeds.Para.Id,
        Author = AuthorSeeds.Para,
        FilesInAlbums = []
    };
    public static AlbumEntity ParaPara = new()
    {
        Id = Guid.Parse("89f94eca-aed8-4ac7-bee5-127b6b90b278"),
        AlbumName = "Para",
        Description = "",
        Picture = "Resources\\Images\\Albums\\Para (by Para).jpg",
        Year = 2007,
        AuthorId = AuthorSeeds.Para.Id,
        Author = AuthorSeeds.Para,
        FilesInAlbums = []
    };
    
    public static AlbumEntity HorkyzeKyzeSliz = new()
    {
        Id = Guid.Parse("3C123C6C-47D9-48D0-BD50-03E80EE550CA"),
        AlbumName = "Kýže sliz",
        Description = "",
        Picture = "Resources\\Images\\Albums\\Kýže Sliz (by Horkýže Slíže).jpg",
        Year = 2002,
        AuthorId = AuthorSeeds.HorkyzeSlize.Id,
        Author = AuthorSeeds.HorkyzeSlize,
        FilesInAlbums = []
    };
    
    public static DbContext SeedAlbums(this DbContext dbx)
    {
        dbx.Set<AlbumEntity>().AddRange(
            ParaPreVsetkoOKoloNas with{Author = null!},
            ParaNasouKrajinou with{Author = null!},
            ParaPara with{Author = null!},
            HorkyzeKyzeSliz with{Author = null!}
        );

        return dbx;
    }
}