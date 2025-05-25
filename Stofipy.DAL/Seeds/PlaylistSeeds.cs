using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;

namespace Stofipy.DAL.Seeds;

public static class PlaylistSeeds
{
    public static PlaylistEntity MyFavourites = new()
    {
        Id = Guid.Parse("E8C2615C-51E5-41E6-A864-7620A17C9D78"),
        PlaylistName = "My Favourites",
        Description = "",
        Picture = null,
        FilesInPlaylists = []
    };
    public static PlaylistEntity TenerifeVacay = new()
    {
        Id = Guid.Parse("949D345A-E9A6-4FC7-98BD-2E27ABF3C799"),
        PlaylistName = "TenerifeVacay",
        Description = "",
        Picture = null,
        FilesInPlaylists = []
    };
    public static PlaylistEntity EasyRun = new()
    {
        Id = Guid.Parse("8CE7C7E6-F712-494C-A9ED-573DE2AFBFA4"),
        PlaylistName = "easy run",
        Description = "",
        Picture = null,
        FilesInPlaylists = []
    };

    public static PlaylistEntity MixForToday = new()
    {
        Id = Guid.Parse("C1574686-6CFE-4B95-BC9F-0143DE45A78B"),
        PlaylistName = "Mix na dnes",
        Description = "",
        Picture = null,
        FilesInPlaylists = []
    };

    public static DbContext SeedPlaylists(this DbContext dbx)
    {
        dbx.Set<PlaylistEntity>().AddRange(
            MyFavourites,
            TenerifeVacay,
            EasyRun,
            MixForToday
        );
        return dbx;
    }
}