using Microsoft.EntityFrameworkCore;
using Sotify.DAL.Entities;

namespace Sotify.DAL.Seeds;

public static class AuthorSeeds
{
    public static AuthorEntity HorkyzeSlize = new()
    {
        Id = Guid.Parse("63e304c0-f051-436d-a466-3048c1c0d31f"),
        AuthorName = "Horkýže Slíže",
        Files = []
    };

    public static AuthorEntity PitstopBoys = new()
    {
        Id = Guid.Parse("f1e4168e-0652-4856-8959-bcc7af725ab7"),
        AuthorName = "Pitstop Boys",
        Files = []
    };

    public static AuthorEntity Para = new()
    {
        Id = Guid.Parse("8e9db5b9-0259-4cca-8949-1871beeef14b"),
        AuthorName = "Para",
        Files = []
    };
    
    public static DbContext SeedAuthors(this DbContext dbx)
    {
        dbx.Set<AuthorEntity>().AddRange(
            HorkyzeSlize,
            PitstopBoys,
            Para
        );

        return dbx;
    }
}