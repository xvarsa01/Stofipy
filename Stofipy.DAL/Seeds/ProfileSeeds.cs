using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;

namespace Stofipy.DAL.Seeds;

public static class ProfileSeeds
{
    public static ProfileEntity User1 = new()
    {
        Id = Guid.Parse("2E9CC54B-F6C5-4763-8829-41A25FB1EB78"),
        Name = "User1",
        Followings = [],
        Followers = [],
        CreatedPlaylists = [],
    };
    public static ProfileEntity User2 = new()
    {
        Id = Guid.Parse("D87994B2-7630-43EC-A6E3-81ED04D9AE8E"),
        Name = "User1",
        Followings = [],
        Followers = [],
        CreatedPlaylists = [],
    };
    public static ProfileEntity User3 = new()
    {
        Id = Guid.Parse("2D33C52C-4554-430E-901B-0225F6202D54"),
        Name = "User1",
        Followings = [],
        Followers = [],
        CreatedPlaylists = [],
    };
    
    
    public static DbContext SeedProfiles(this DbContext dbx)
    {
        dbx.Set<ProfileEntity>().AddRange(
            User1,
            User2,
            User3
        );
        return dbx;
    }
}