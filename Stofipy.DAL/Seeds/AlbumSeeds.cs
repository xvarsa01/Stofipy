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
        Picture = GetFullPathFromName("pre_vsetko_okolo_nas.jpg"),
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
        Picture = GetFullPathFromName("nasou_krajinou.jpg"),
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
        Picture = GetFullPathFromName("para_album.jpg"),
        Year = 2007,
        AuthorId = AuthorSeeds.Para.Id,
        Author = AuthorSeeds.Para,
        FilesInAlbums = []
    };
    
    
    // horkyze
    public static AlbumEntity Kýže_Sliz = new()
    {
        Id = Guid.Parse("3C123C6C-47D9-48D0-BD50-03E80EE550CA"),
        AlbumName = "Kýže sliz",
        Description = "",
        Picture = GetFullPathFromName("kyze_sliz.jpg"),
        Year = 2002,
        AuthorId = AuthorSeeds.HorkyzeSlize.Id,
        Author = AuthorSeeds.HorkyzeSlize,
        FilesInAlbums = []
    };
    public static AlbumEntity Ukáž_tú_tvoju_ZOO = new()
    {
        Id = Guid.Parse("59ccc168-39d5-4bcb-9303-7226d9288cac"),
        AlbumName = "Ukáž tú tvoju ZOO",
        Description = "",
        Picture = GetFullPathFromName("ukaz_tu_tvoju_zoo.jpg"),
        Year = 2007,
        AuthorId = AuthorSeeds.HorkyzeSlize.Id,
        Author = AuthorSeeds.HorkyzeSlize,
        FilesInAlbums = []
    };
    public static AlbumEntity St_Mary_Huana_Ganja = new()
    {
        Id = Guid.Parse("3f42577d-058b-4773-9a2d-d4c9cd7148d6"),
        AlbumName = "St. Mary Huana Ganja",
        Description = "",
        Picture = GetFullPathFromName("st_mary_huana_ganja.jpg"),
        Year = 2012,
        AuthorId = AuthorSeeds.HorkyzeSlize.Id,
        Author = AuthorSeeds.HorkyzeSlize,
        FilesInAlbums = []
    };
    public static AlbumEntity PUSTITE_KAROLA = new()
    {
        Id = Guid.Parse("d2550e63-f35a-45a7-a820-0be366f5227f"),
        AlbumName = "PUSTITE KAROLA!",
        Description = "",
        Picture = GetFullPathFromName("pustite_karola.jpg"),
        Year = 2017,
        AuthorId = AuthorSeeds.HorkyzeSlize.Id,
        Author = AuthorSeeds.HorkyzeSlize,
        FilesInAlbums = []
    };
    public static AlbumEntity Alibaba_a_40_krátkych_songov_2 = new()
    {
        Id = Guid.Parse("344bfc75-0814-48ed-aa14-053ab0aeab8a"),
        AlbumName = "Alibaba a 40 krátkych songov 2",
        Description = "",
        Picture = GetFullPathFromName("alibaba_a_40_kratkych_songov.jpg"),
        Year = 2021,
        AuthorId = AuthorSeeds.HorkyzeSlize.Id,
        Author = AuthorSeeds.HorkyzeSlize,
        FilesInAlbums = []
    };
    public static AlbumEntity Právo_Veta = new()
    {
        Id = Guid.Parse("1f9cfd40-b74a-4097-9d57-4564efa53404"),
        AlbumName = "Právo Veta",
        Description = "",
        Picture = GetFullPathFromName("pravo_veta.jpg"),
        Year = 2022,
        AuthorId = AuthorSeeds.HorkyzeSlize.Id,
        Author = AuthorSeeds.HorkyzeSlize,
        FilesInAlbums = []
    };
    public static AlbumEntity Tvojho_brata_brat_má_brata = new()
    {
        Id = Guid.Parse("77476871-a3fe-4007-8002-7939caa31d30"),
        AlbumName = "Tvojho brata brat má brata.jpg",
        Description = "",
        Picture = GetFullPathFromName("tvojho_brata_brat_ma_brata.jpg"),
        Year = 2022,
        AuthorId = AuthorSeeds.HorkyzeSlize.Id,
        Author = AuthorSeeds.HorkyzeSlize,
        FilesInAlbums = []
    };
    public static AlbumEntity RAMPA = new()
    {
        Id = Guid.Parse("151f9848-fbb8-4f80-b217-c2ad0e5ae8f7"),
        AlbumName = "RAMPA",
        Description = "",
        Picture = GetFullPathFromName("rampa.jpg"),
        Year = 2021,
        AuthorId = AuthorSeeds.HorkyzeSlize.Id,
        Author = AuthorSeeds.HorkyzeSlize,
        FilesInAlbums = []
    };
    
    // Maneskin
    public static AlbumEntity Teatro_Dira_Vol_I = new()
    {
        Id = Guid.Parse("cda9b03d-42eb-48b5-9a65-d18aec670e4b"),
        AlbumName = "Teatro D'ira - Vol. I",
        Description = "",
        Picture = GetFullPathFromName("teatro_dira_vol_i.jpg"),
        Year = 2002,
        AuthorId = AuthorSeeds.Maneskin.Id,
        Author = AuthorSeeds.Maneskin,
        FilesInAlbums = []
    };
    
    public static DbContext SeedAlbums(this DbContext dbx)
    {
        dbx.Set<AlbumEntity>().AddRange(
            ParaPreVsetkoOKoloNas with{Author = null!},
            ParaNasouKrajinou with{Author = null!},
            ParaPara with{Author = null!},
            
            Kýže_Sliz with{Author = null!},
            Ukáž_tú_tvoju_ZOO with{Author = null!},
            St_Mary_Huana_Ganja with{Author = null!},
            PUSTITE_KAROLA with{Author = null!},
            Alibaba_a_40_krátkych_songov_2 with{Author = null!},
            Právo_Veta with{Author = null!},
            Tvojho_brata_brat_má_brata with{Author = null!},
            RAMPA with{Author = null!},
            
            Teatro_Dira_Vol_I with{Author = null!}
        );

        return dbx;
    }

    private static string GetFullPathFromName(string name)
    {
        return Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "Stofipy", "Media", "Images", "Albums", name);
    }
}