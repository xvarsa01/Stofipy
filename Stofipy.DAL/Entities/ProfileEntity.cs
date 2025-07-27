namespace Stofipy.DAL.Entities;

public record ProfileEntity : IEntity
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public string? ProfilePicture { get; set; }
    public ICollection<ProfileFollowingProfileEntity> Followings { get; set; } = [];
    public ICollection<ProfileFollowingProfileEntity> Followers { get; set; } = [];
    
    public ICollection<ProfileFollowingAuthorEntity> FollowingAuthors { get; set; } = [];
    public ICollection<PlaylistEntity> CreatedPlaylists { get; set; } = [];

    public void FollowNewProfile(ProfileFollowingProfileEntity profile)
    {
        Followings.Add(profile);
    }

    public void UnfollowProfile(ProfileFollowingProfileEntity profile)
    {
        Followings.Remove(profile);
    }

    public void AddFollower(ProfileFollowingProfileEntity profile)
    {
        Followers.Add(profile);
    }

    public void RemoveFollower(ProfileFollowingProfileEntity profile)
    {
        Followings.Remove(profile);
    }

    public void FollowArtist(ProfileFollowingAuthorEntity author)
    {
        FollowingAuthors.Add(author);
    }

    public void UnfollowArtist(ProfileFollowingAuthorEntity author)
    {
        FollowingAuthors.Remove(author);
    }
}