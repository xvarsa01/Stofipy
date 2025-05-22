using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;

namespace Stofipy.DAL.Seeds;

public static class AuthorSeeds
{
    public static AuthorEntity BillieEilish = new()
    {
        Id = Guid.Parse("456cc531-6e05-428c-abaa-10565a6c2b81"),
        AuthorName = "Billie Eilish",
        ProfilePicture = "billie_eilish.jpg",
        Files = []
    };

    public static AuthorEntity BillyBarman = new()
    {
        Id = Guid.Parse("91c12af9-8c2f-4fa6-885f-46b55f50b35e"),
        AuthorName = "Billy Barman",
        ProfilePicture = "billy_barman.jpg",
        Files = []
    };

    public static AuthorEntity BrunoMars = new()
    {
        Id = Guid.Parse("e5750a4b-9ca2-4fa3-bd2c-bd9fe8c9b082"),
        AuthorName = "Bruno Mars",
        ProfilePicture = "bruno_mars.jpg",
        Files = []
    };

    public static AuthorEntity DJKhaled = new()
    {
        Id = Guid.Parse("b142557d-df88-4185-a535-55ded6f9658c"),
        AuthorName = "DJ Khaled",
        ProfilePicture = "dj_khaled.jpg",
        Files = []
    };

    public static AuthorEntity Gleb = new()
    {
        Id = Guid.Parse("88f2f713-b3a7-4508-bd88-2f52627dd391"),
        AuthorName = "Gleb",
        ProfilePicture = "gleb.jpg",
        Files = []
    };

    public static AuthorEntity HelenineOci = new()
    {
        Id = Guid.Parse("0c7cb20c-7540-4aef-b975-fe8385678278"),
        AuthorName = "Heľenine Oči",
        ProfilePicture = "helenine_oci.jpg",
        Files = []
    };

    public static AuthorEntity HorkyzeSlize = new()
    {
        Id = Guid.Parse("63e304c0-f051-436d-a466-3048c1c0d31f"),
        AuthorName = "Horkýže Slíže",
        ProfilePicture = "horkyze_slize.jpg",
        Files = []
    };

    public static AuthorEntity Maneskin = new()
    {
        Id = Guid.Parse("29656df8-a03a-4572-84c8-73af305ee4c5"),
        AuthorName = "Måneskin",
        ProfilePicture = "maneskin.jpg",
        Files = []
    };

    public static AuthorEntity OliviaRodrigo = new()
    {
        Id = Guid.Parse("7a24e8e7-5256-4fa1-9682-6e702ce58522"),
        AuthorName = "Olivia Rodrigo",
        ProfilePicture = "olivia_rodrigo.jpg",
        Files = []
    };
    
    public static AuthorEntity Para = new()
    {
        Id = Guid.Parse("16c4d93a-8eef-4778-a624-f2f321ad2a40"),
        AuthorName = "Para",
        ProfilePicture = "para_author.jpg",
        Files = []
    };

    public static AuthorEntity PitstopBoys = new()
    {
        Id = Guid.Parse("f1e4168e-0652-4856-8959-bcc7af725ab7"),
        AuthorName = "Pitstop Boys",
        ProfilePicture = "pitstop_boys.jpg",
        Files = []
    };
    
    public static DbContext SeedAuthors(this DbContext dbx)
    {
        dbx.Set<AuthorEntity>().AddRange(
            BillieEilish,
            BillyBarman,
            BrunoMars,
            DJKhaled,
            Gleb,
            HelenineOci,
            HorkyzeSlize,
            Maneskin,
            OliviaRodrigo,
            PitstopBoys,
            Para
        );

        return dbx;
    }
}
