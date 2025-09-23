using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;

namespace Stofipy.Common.Tests.Seeds;

public static class ProfileTestSeeds
{
    public static ProfileEntity User1 = new()
    {
        Id = Guid.Parse("3341a638-a740-4c64-947e-c287cf9be1e0"),
        Name = "User1",
        Followings = [],
        Followers = [],
        FollowingAuthors = [],
        CreatedPlaylists = [],
    };
    public static ProfileEntity User2 = new()
    {
        Id = Guid.Parse("24fc5208-3e31-40da-bb8e-9f0fe7789d4d"),
        Name = "User1",
        Followings = [],
        Followers = [],
        FollowingAuthors = [],
        CreatedPlaylists = [],
    };
    public static ProfileEntity User3 = new()
    {
        Id = Guid.Parse("86a2fed7-11ad-462c-b9f5-aa210d5b53c9"),
        Name = "User1",
        Followings = [],
        Followers = [],
        FollowingAuthors = [],
        CreatedPlaylists = [],
    };
    
    
    public static DbContext SeedTestProfiles(this DbContext dbx)
    {
        dbx.Set<ProfileEntity>().AddRange(
            User1 with{Followings = [], Followers = [], FollowingAuthors = [], CreatedPlaylists = []},
            User2 with{Followings = [], Followers = [], FollowingAuthors = [], CreatedPlaylists = []},
            User3 with{Followings = [], Followers = [], FollowingAuthors = [], CreatedPlaylists = []}
        );
        return dbx;
    }
}