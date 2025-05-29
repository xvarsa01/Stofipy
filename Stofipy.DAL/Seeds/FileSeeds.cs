using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;
using Stofipy.DAL.Enums;

namespace Stofipy.DAL.Seeds;

public static class FileSeeds
{
    // Para - Pre Vsetkych Okolo Nas
    public static FileEntity NavzdyTvoj = new()
    {
        Id = Guid.Parse("9336B78A-264C-4DD5-938B-8165BE3CE5DE"),
        FileName = "Navždy tvoj",
        Description = "",
        Lyrics = null,
        Size = 0,
        Length = 236,
        PlayCount = 268_561,
        Category = Category.Rock,
        AuthorId = AuthorSeeds.Para.Id,
        Author = AuthorSeeds.Para,
        FilesInPlaylists = [],
        FilesInAlbums = [],
        DefaultAlbumId = AlbumSeeds.ParaPreVsetkoOKoloNas.Id,
        DefaultAlbum = AlbumSeeds.ParaPreVsetkoOKoloNas,
    };
    
    public static FileEntity Vilma = new()
    {
        Id = Guid.Parse("46E4C679-2AFE-49DA-897C-26E551BE2FA3"),
        FileName = "Vilma",
        Description = "",
        Lyrics = null,
        Size = 0,
        Length = 234,
        PlayCount = 415_752,
        Category = Category.Rock,
        AuthorId = AuthorSeeds.Para.Id,
        Author = AuthorSeeds.Para,
        FilesInPlaylists = [],
        FilesInAlbums = [],
        DefaultAlbumId = AlbumSeeds.ParaPreVsetkoOKoloNas.Id,
        DefaultAlbum = AlbumSeeds.ParaPreVsetkoOKoloNas,
    };

    public static FileEntity ChcemIstDomov = new()
    {
        Id = Guid.Parse("F42DC111-E7C2-4E05-BA8F-A8A800D106E1"),
        FileName = "Chcem ísť domov",
        Description = "",
        Lyrics = null,
        Size = 0,
        Length = 186,
        PlayCount = 256_512,
        Category = Category.Rock,
        AuthorId = AuthorSeeds.Para.Id,
        Author = AuthorSeeds.Para,
        FilesInPlaylists = [],
        FilesInAlbums = [],
        DefaultAlbumId = AlbumSeeds.ParaPreVsetkoOKoloNas.Id,
        DefaultAlbum = AlbumSeeds.ParaPreVsetkoOKoloNas,
    };
    
    public static FileEntity Cas = new()
    {
        Id = Guid.Parse("E5AD9869-2713-4196-AC79-41ADD73FB2A6"),
        FileName = "Čas",
        Description = "",
        Lyrics = null,
        Size = 0,
        Length = 233,
        PlayCount = 734_951,
        Category = Category.Rock,
        AuthorId = AuthorSeeds.Para.Id,
        Author = AuthorSeeds.Para,
        FilesInPlaylists = [],
        FilesInAlbums = [],
        DefaultAlbumId = AlbumSeeds.ParaPreVsetkoOKoloNas.Id,
        DefaultAlbum = AlbumSeeds.ParaPreVsetkoOKoloNas,
    };
    // a
    public static FileEntity PreTychCoOstali = new()
    {
        Id = Guid.Parse("05D59FF1-4F7E-43C1-A1F9-635661C15245"),
        FileName = "Pre Tých Čo Ostali",
        Description = "",
        Lyrics = null,
        Size = 0,
        Length = 197,
        PlayCount = 449_364,
        Category = Category.Rock,
        AuthorId = AuthorSeeds.Para.Id,
        Author = AuthorSeeds.Para,
        FilesInPlaylists = [],
        FilesInAlbums = [],
        DefaultAlbumId = AlbumSeeds.ParaPreVsetkoOKoloNas.Id,
        DefaultAlbum = AlbumSeeds.ParaPreVsetkoOKoloNas,
    };
    public static FileEntity ToOkoloNas = new()
    {
        Id = Guid.Parse("3400D0AF-BA63-4FD9-9213-24B476EC7632"),
        FileName = "To Okolo Nás",
        Description = "",
        Lyrics = null,
        Size = 0,
        Length = 257,
        PlayCount = 887_026,
        Category = Category.Rock,
        AuthorId = AuthorSeeds.Para.Id,
        Author = AuthorSeeds.Para,
        FilesInPlaylists = [],
        FilesInAlbums = [],
        DefaultAlbumId = AlbumSeeds.ParaPreVsetkoOKoloNas.Id,
        DefaultAlbum = AlbumSeeds.ParaPreVsetkoOKoloNas,
    };
    public static FileEntity JedenDenVKyjeve = new()
    {
        Id = Guid.Parse("1E010C2F-DD4E-4226-B3A6-0C8A9455B94B"),
        FileName = "Jeden Deň V Kyjeve",
        Description = "",
        Lyrics = null,
        Size = 0,
        Length = 253,
        PlayCount = 196_561,
        Category = Category.Rock,
        AuthorId = AuthorSeeds.Para.Id,
        Author = AuthorSeeds.Para,
        FilesInPlaylists = [],
        FilesInAlbums = [],
        DefaultAlbumId = AlbumSeeds.ParaPreVsetkoOKoloNas.Id,
        DefaultAlbum = AlbumSeeds.ParaPreVsetkoOKoloNas,
    };
    public static FileEntity NaCeste = new()
    {
        Id = Guid.Parse("1ED237FC-5F04-48F1-B659-B2D5FE8B8E0C"),
        FileName = "Na Ceste",
        Description = "",
        Lyrics = null,
        Size = 0,
        Length = 177,
        PlayCount = 249_448,
        Category = Category.Rock,
        AuthorId = AuthorSeeds.Para.Id,
        Author = AuthorSeeds.Para,
        FilesInPlaylists = [],
        FilesInAlbums = [],
        DefaultAlbumId = AlbumSeeds.ParaPreVsetkoOKoloNas.Id,
        DefaultAlbum = AlbumSeeds.ParaPreVsetkoOKoloNas,
    };
    public static FileEntity StaneSa = new()
    {
        Id = Guid.Parse("3AA912E0-E82A-4B81-8FBE-6E7C65953216"),
        FileName = "Stane Sa",
        Description = "",
        Lyrics = null,
        Size = 0,
        Length = 222,
        PlayCount = 163_232,
        Category = Category.Rock,
        AuthorId = AuthorSeeds.Para.Id,
        Author = AuthorSeeds.Para,
        FilesInPlaylists = [],
        FilesInAlbums = [],
        DefaultAlbumId = AlbumSeeds.ParaPreVsetkoOKoloNas.Id,
        DefaultAlbum = AlbumSeeds.ParaPreVsetkoOKoloNas,
    };
    public static FileEntity PreJonihovsetkyDeti = new()
    {
        Id = Guid.Parse("96EE276E-FDF1-446F-9BD3-916CCE491C88"),
        FileName = "Pre Joniho Všetky Deti",
        Description = "",
        Lyrics = null,
        Size = 0,
        Length = 259,
        PlayCount = 159_561,
        Category = Category.Rock,
        AuthorId = AuthorSeeds.Para.Id,
        Author = AuthorSeeds.Para,
        FilesInPlaylists = [],
        FilesInAlbums = [],
        DefaultAlbumId = AlbumSeeds.ParaPreVsetkoOKoloNas.Id,
        DefaultAlbum = AlbumSeeds.ParaPreVsetkoOKoloNas,
    };
    
    // Para Nasou Krajinou
    public static FileEntity Svadobna = new()
    {
        Id = Guid.Parse("C54B0D9B-E4A8-40D2-88A1-F1501E8F5134"),
        FileName = "Svadobná",
        Description = "",
        Lyrics = null,
        Size = 0,
        Length = 293,
        PlayCount = 863_657,
        Category = Category.Rock,
        AuthorId = AuthorSeeds.Para.Id,
        Author = AuthorSeeds.Para,
        FilesInPlaylists = [],
        FilesInAlbums = [],
        DefaultAlbumId = AlbumSeeds.ParaNasouKrajinou.Id,
        DefaultAlbum = AlbumSeeds.ParaNasouKrajinou,
    };
    public static FileEntity NasouKrajinou = new()
    {
        Id = Guid.Parse("ABEFB9A2-5FB1-4C74-AF16-531B2C8E7145"),
        FileName = "Našou Krajinou",
        Description = "",
        Lyrics = null,
        Size = 0,
        Length = 301,
        PlayCount = 658_596,
        Category = Category.Rock,
        AuthorId = AuthorSeeds.Para.Id,
        Author = AuthorSeeds.Para,
        FilesInPlaylists = [],
        FilesInAlbums = [],
        DefaultAlbumId = AlbumSeeds.ParaNasouKrajinou.Id,
        DefaultAlbum = AlbumSeeds.ParaNasouKrajinou,
    };
    public static FileEntity Nostalgia = new()
    {
        Id = Guid.Parse("B7C5870C-B55B-4F49-AB30-4A4A41E59DD3"),
        FileName = "Nostalgia",
        Description = "",
        Lyrics = null,
        Size = 0,
        PlayCount = 558_501,
        Length = 219,
        Category = Category.Rock,
        AuthorId = AuthorSeeds.Para.Id,
        Author = AuthorSeeds.Para,
        FilesInPlaylists = [],
        FilesInAlbums = [],
        DefaultAlbumId = AlbumSeeds.ParaNasouKrajinou.Id,
        DefaultAlbum = AlbumSeeds.ParaNasouKrajinou,
    };
    public static FileEntity Miesta = new()
    {
        Id = Guid.Parse("61133A66-56D4-43B9-9FBE-725DB02FF8DC"),
        FileName = "Miesta",
        Description = "",
        Lyrics = null,
        Size = 0,
        Length = 191,
        PlayCount = 338_207,
        Category = Category.Rock,
        AuthorId = AuthorSeeds.Para.Id,
        Author = AuthorSeeds.Para,
        FilesInPlaylists = [],
        FilesInAlbums = [],
        DefaultAlbumId = AlbumSeeds.ParaNasouKrajinou.Id,
        DefaultAlbum = AlbumSeeds.ParaNasouKrajinou,
    };
    
    
    // Para - Para
    public static FileEntity Abstinent = new()
    {
        Id = Guid.Parse("fa265974-80e1-41af-8e82-e16fd19f8d94"),
        FileName = "Abstinent",
        Description = "",
        Lyrics = null,
        Size = 0,
        Length = 175,
        PlayCount = 937_520,
        Category = Category.Rock,
        AuthorId = AuthorSeeds.Para.Id,
        Author = AuthorSeeds.Para,
        FilesInPlaylists = [],
        FilesInAlbums = [],
        DefaultAlbumId = AlbumSeeds.ParaPara.Id,
        DefaultAlbum = AlbumSeeds.ParaPara,
    };
    
    // Horkyze Slize - Kyze Sliz
    public static FileEntity MalaZuzu = new()
    {
        Id = Guid.Parse("c616ed5d-36fb-40af-b560-7b520391fc47"),
        FileName = "Mala Žužu",
        Description = "",
        Picture = "Resources\\Images\\Albums\\Kýže Sliz (by Horkýže Slíže).jpg",
        Lyrics = null,
        Size = 0,
        PlayCount = 1562_657,
        Length = 86,
        Category = Category.Rock,
        AuthorId = AuthorSeeds.HorkyzeSlize.Id,
        Author = AuthorSeeds.HorkyzeSlize,
        FilesInPlaylists = [],
        FilesInAlbums = [],
        DefaultAlbumId = AlbumSeeds.Kýže_Sliz.Id,
        DefaultAlbum = AlbumSeeds.Kýže_Sliz,
    };
    public static FileEntity Jogin = new()
    {
        Id = Guid.Parse("40083198-7605-4f29-9826-acc8c014e024"),
        FileName = "Jogín",
        Description = "",
        Lyrics = null,
        Size = 0,
        Length = 222,
        PlayCount = 669_630,
        Category = Category.Rock,
        AuthorId = AuthorSeeds.HorkyzeSlize.Id,
        Author = AuthorSeeds.HorkyzeSlize,
        FilesInPlaylists = [],
        FilesInAlbums = [],
        DefaultAlbumId = AlbumSeeds.Kýže_Sliz.Id,
        DefaultAlbum = AlbumSeeds.Kýže_Sliz,
    };
    public static FileEntity Vlak = new()
    {
        Id = Guid.Parse("812438a2-58e2-4dc4-9324-145f4f301f6a"),
        FileName = "Vlak",
        Description = "",
        Lyrics = null,
        Size = 0,
        Length = 171,
        PlayCount = 1530_059,
        Category = Category.Rock,
        AuthorId = AuthorSeeds.HorkyzeSlize.Id,
        Author = AuthorSeeds.HorkyzeSlize,
        FilesInPlaylists = [],
        FilesInAlbums = [],
        DefaultAlbumId = AlbumSeeds.Kýže_Sliz.Id,
        DefaultAlbum = AlbumSeeds.Kýže_Sliz,
    };
    
    // Horkyze Slize - Ukáž_tú_tvoju_ZOO
    public static FileEntity JaZeruKviti = new()
    {
        Id = Guid.Parse("6777bf1e-76c0-494b-b5dd-1fdcdda4354a"),
        FileName = "Já žeru Kvítí",
        Description = "",
        Lyrics = null,
        Size = 0,
        Length = 160,
        PlayCount = 682_470,
        Category = Category.Rock,
        AuthorId = AuthorSeeds.HorkyzeSlize.Id,
        Author = AuthorSeeds.HorkyzeSlize,
        FilesInPlaylists = [],
        FilesInAlbums = [],
        DefaultAlbumId = AlbumSeeds.Ukáž_tú_tvoju_ZOO.Id,
        DefaultAlbum = AlbumSeeds.Ukáž_tú_tvoju_ZOO,
    };
    public static FileEntity RnBsoul = new()
    {
        Id = Guid.Parse("0dc00af9-f750-4e50-9821-299bd76f3596"),
        FileName = "R m'B soul",
        Description = "",
        Lyrics = null,
        Size = 0,
        Length = 169,
        PlayCount = 321_561,
        Category = Category.Rock,
        AuthorId = AuthorSeeds.HorkyzeSlize.Id,
        Author = AuthorSeeds.HorkyzeSlize,
        FilesInPlaylists = [],
        FilesInAlbums = [],
        DefaultAlbumId = AlbumSeeds.Ukáž_tú_tvoju_ZOO.Id,
        DefaultAlbum = AlbumSeeds.Ukáž_tú_tvoju_ZOO,
    };
    public static FileEntity KomisarRex = new()
    {
        Id = Guid.Parse("b31847dd-6730-48a7-8351-058d364f5c69"),
        FileName = "Komisár Rex",
        Description = "",
        Lyrics = null,
        Size = 0,
        Length = 116,
        PlayCount = 210_561,
        Category = Category.Rock,
        AuthorId = AuthorSeeds.HorkyzeSlize.Id,
        Author = AuthorSeeds.HorkyzeSlize,
        FilesInPlaylists = [],
        FilesInAlbums = [],
        DefaultAlbumId = AlbumSeeds.Ukáž_tú_tvoju_ZOO.Id,
        DefaultAlbum = AlbumSeeds.Ukáž_tú_tvoju_ZOO,
    };
    public static FileEntity SilnyRefren = new()
    {
        Id = Guid.Parse("c1f66e7f-ad44-4e5f-8ded-d1bf6637630a"),
        FileName = "Silný Refrén",
        Description = "",
        Lyrics = null,
        Size = 0,
        Length = 188,
        PlayCount = 1468_987,
        Category = Category.Rock,
        AuthorId = AuthorSeeds.HorkyzeSlize.Id,
        Author = AuthorSeeds.HorkyzeSlize,
        FilesInPlaylists = [],
        FilesInAlbums = [],
        DefaultAlbumId = AlbumSeeds.Ukáž_tú_tvoju_ZOO.Id,
        DefaultAlbum = AlbumSeeds.Ukáž_tú_tvoju_ZOO,
    };
    
    // Horkyze Slize - St_Mary_Huana_Ganja
    public static FileEntity Intro = new()
    {
        Id = Guid.Parse("6e6f6c99-bbb2-4c01-b414-87b0d4faf642"),
        FileName = "Intro",
        Description = "",
        Lyrics = null,
        Size = 0,
        Length = 86,
        PlayCount = 1244_601,
        Category = Category.Rock,
        AuthorId = AuthorSeeds.HorkyzeSlize.Id,
        Author = AuthorSeeds.HorkyzeSlize,
        FilesInPlaylists = [],
        FilesInAlbums = [],
        DefaultAlbumId = AlbumSeeds.St_Mary_Huana_Ganja.Id,
        DefaultAlbum = AlbumSeeds.St_Mary_Huana_Ganja,
    };
    public static FileEntity Nerob = new()
    {
        Id = Guid.Parse("e0d6ee09-8150-4add-9947-9fdd66ee535c"),
        FileName = "Nerob!!!",
        Description = "",
        Lyrics = null,
        Size = 0,
        Length = 139,
        PlayCount = 778_563,
        Category = Category.Rock,
        AuthorId = AuthorSeeds.HorkyzeSlize.Id,
        Author = AuthorSeeds.HorkyzeSlize,
        FilesInPlaylists = [],
        FilesInAlbums = [],
        DefaultAlbumId = AlbumSeeds.St_Mary_Huana_Ganja.Id,
        DefaultAlbum = AlbumSeeds.St_Mary_Huana_Ganja,
    };
    public static FileEntity Nazdar = new()
    {
        Id = Guid.Parse("70abea41-b43e-4257-9f4b-baa8ff7ac20a"),
        FileName = "Nazdar!!!",
        Description = "",
        Lyrics = null,
        Size = 0,
        Length = 171,
        PlayCount = 961_001,
        Category = Category.Rock,
        AuthorId = AuthorSeeds.HorkyzeSlize.Id,
        Author = AuthorSeeds.HorkyzeSlize,
        FilesInPlaylists = [],
        FilesInAlbums = [],
        DefaultAlbumId = AlbumSeeds.St_Mary_Huana_Ganja.Id,
        DefaultAlbum = AlbumSeeds.St_Mary_Huana_Ganja,
    };
    
    public static DbContext SeedFiles(this DbContext dbx)
    {
        dbx.Set<FileEntity>().AddRange(
            NavzdyTvoj with{Author = null!, DefaultAlbum = null!},
            Vilma with{Author = null!, DefaultAlbum = null!},
            ChcemIstDomov with{Author = null!, DefaultAlbum = null!},
            Cas with{Author = null!, DefaultAlbum = null!},
            PreTychCoOstali with{Author = null!, DefaultAlbum = null!},
            ToOkoloNas with{Author = null!, DefaultAlbum = null!},
            JedenDenVKyjeve with{Author = null!, DefaultAlbum = null!},
            NaCeste with{Author = null!, DefaultAlbum = null!},
            StaneSa with{Author = null!, DefaultAlbum = null!},
            PreJonihovsetkyDeti with{Author = null!, DefaultAlbum = null!},
            
            Svadobna with{Author = null!, DefaultAlbum = null!},
            NasouKrajinou with{Author = null!, DefaultAlbum = null!},
            Nostalgia with{Author = null!, DefaultAlbum = null!},
            Miesta with{Author = null!, DefaultAlbum = null!},
            
            Abstinent with{Author = null!, DefaultAlbum = null!},
            
            MalaZuzu with{Author = null!, DefaultAlbum = null!},
            Jogin with{Author = null!, DefaultAlbum = null!},
            Vlak with{Author = null!, DefaultAlbum = null!},
            
            JaZeruKviti with{Author = null!, DefaultAlbum = null!},
            RnBsoul with{Author = null!, DefaultAlbum = null!},
            KomisarRex with{Author = null!, DefaultAlbum = null!},
            SilnyRefren with{Author = null!, DefaultAlbum = null!},
            
            Intro with{Author = null!, DefaultAlbum = null!},
            Nerob with{Author = null!, DefaultAlbum = null!},
            Nazdar with{Author = null!, DefaultAlbum = null!}
        );

        return dbx;
    }

}