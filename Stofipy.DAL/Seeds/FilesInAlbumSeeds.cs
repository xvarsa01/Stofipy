using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;

namespace Stofipy.DAL.Seeds;

public static class FilesInAlbumSeeds
{
    public static FilesInAlbumEntity PreVsetkychOkoloNasFile1 = new()
    {
        Id = Guid.Parse("4f7f905d-036e-479f-bd3a-1e6aaaa6ec14"),
        AlbumId = AlbumSeeds.ParaPreVsetkoOKoloNas.Id,
        FileId = FileSeeds.NavzdyTvoj.Id,
        Album = AlbumSeeds.ParaPreVsetkoOKoloNas,
        File = FileSeeds.NavzdyTvoj,
        Index = 1
    };
    public static FilesInAlbumEntity PreVsetkychOkoloNasFile2 = new()
    {
        Id = Guid.Parse("495534e8-df0c-4aca-aa61-e491312046ca"),
        AlbumId = AlbumSeeds.ParaPreVsetkoOKoloNas.Id,
        FileId = FileSeeds.Vilma.Id,
        Album = AlbumSeeds.ParaPreVsetkoOKoloNas,
        File = FileSeeds.Vilma,
        Index = 2
    };
    public static FilesInAlbumEntity PreVsetkychOkoloNasFile3 = new()
    {
        Id = Guid.Parse("9cd4725e-c366-42cf-bd15-d772351da16b"),
        AlbumId = AlbumSeeds.ParaPreVsetkoOKoloNas.Id,
        FileId = FileSeeds.ChcemIstDomov.Id,
        Album = AlbumSeeds.ParaPreVsetkoOKoloNas,
        File = FileSeeds.ChcemIstDomov,
        Index = 3
    };
    public static FilesInAlbumEntity PreVsetkychOkoloNasFile4 = new()
    {
        Id = Guid.Parse("aace15e2-c461-4199-b652-94600ee5ca72"),
        AlbumId = AlbumSeeds.ParaPreVsetkoOKoloNas.Id,
        FileId = FileSeeds.Cas.Id,
        Album = AlbumSeeds.ParaPreVsetkoOKoloNas,
        File = FileSeeds.Cas,
        Index = 4
    };
    public static FilesInAlbumEntity PreVsetkychOkoloNasFile5 = new()
    {
        Id = Guid.Parse("f3589f8a-87ae-4d37-8cdf-d697539f370b"),
        AlbumId = AlbumSeeds.ParaPreVsetkoOKoloNas.Id,
        FileId = FileSeeds.PreTychCoOstali.Id,
        Album = AlbumSeeds.ParaPreVsetkoOKoloNas,
        File = FileSeeds.PreTychCoOstali,
        Index = 5
    };
    public static FilesInAlbumEntity PreVsetkychOkoloNasFile6 = new()
    {
        Id = Guid.Parse("7938810a-3058-48f8-b232-ca9de64eb91a"),
        AlbumId = AlbumSeeds.ParaPreVsetkoOKoloNas.Id,
        FileId = FileSeeds.ToOkoloNas.Id,
        Album = AlbumSeeds.ParaPreVsetkoOKoloNas,
        File = FileSeeds.ToOkoloNas,
        Index = 6
    };
    public static FilesInAlbumEntity PreVsetkychOkoloNasFile7 = new()
    {
        Id = Guid.Parse("42e4c7b6-d791-4c66-b34f-7806c5bf0fd2"),
        AlbumId = AlbumSeeds.ParaPreVsetkoOKoloNas.Id,
        FileId = FileSeeds.JedenDenVKyjeve.Id,
        Album = AlbumSeeds.ParaPreVsetkoOKoloNas,
        File = FileSeeds.JedenDenVKyjeve,
        Index = 7
    };
    public static FilesInAlbumEntity PreVsetkychOkoloNasFile8 = new()
    {
        Id = Guid.Parse("860bde48-dc3e-40c8-936a-25384b3833cc"),
        AlbumId = AlbumSeeds.ParaPreVsetkoOKoloNas.Id,
        FileId = FileSeeds.NaCeste.Id,
        Album = AlbumSeeds.ParaPreVsetkoOKoloNas,
        File = FileSeeds.NaCeste,
        Index = 8
    };
    public static FilesInAlbumEntity PreVsetkychOkoloNasFile9 = new()
    {
        Id = Guid.Parse("0d271033-bf25-43f3-b9ef-46daaf080a61"),
        AlbumId = AlbumSeeds.ParaPreVsetkoOKoloNas.Id,
        FileId = FileSeeds.StaneSa.Id,
        Album = AlbumSeeds.ParaPreVsetkoOKoloNas,
        File = FileSeeds.StaneSa,
        Index = 9
    };
    public static FilesInAlbumEntity PreVsetkychOkoloNasFile10 = new()
    {
        Id = Guid.Parse("05f27607-98be-43c6-b21a-6b8d4eb456d6"),
        AlbumId = AlbumSeeds.ParaPreVsetkoOKoloNas.Id,
        FileId = FileSeeds.PreJonihovsetkyDeti.Id,
        Album = AlbumSeeds.ParaPreVsetkoOKoloNas,
        File = FileSeeds.PreJonihovsetkyDeti,
        Index = 10
    };
    
    public static DbContext SeedFilesInAlbums(this DbContext dbx)
    {
        dbx.Set<FilesInAlbumEntity>().AddRange(
            PreVsetkychOkoloNasFile1 with{Album = null!, File = null!},
            PreVsetkychOkoloNasFile2 with{Album = null!, File = null!},
            PreVsetkychOkoloNasFile3 with{Album = null!, File = null!},
            PreVsetkychOkoloNasFile4 with{Album = null!, File = null!},
            PreVsetkychOkoloNasFile5 with{Album = null!, File = null!},
            PreVsetkychOkoloNasFile6 with{Album = null!, File = null!},
            PreVsetkychOkoloNasFile7 with{Album = null!, File = null!},
            PreVsetkychOkoloNasFile8 with{Album = null!, File = null!},
            PreVsetkychOkoloNasFile9 with{Album = null!, File = null!},
            PreVsetkychOkoloNasFile10 with{Album = null!, File = null!}
        );

        return dbx;
    }

}