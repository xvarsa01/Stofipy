using Stofipy.BL.Models.InterfaceBase;

namespace Stofipy.BL.Models;

public record ProfileDetailModel : ModelBase
{
    public required string Name { get; set; }
    public string? ProfilePicture { get; set; }
    public ICollection<ProfileFollowingProfileModel> Following { get; set; } = [];
    public ICollection<ProfileFollowingProfileModel> Followers { get; set; } = [];
    
    public ICollection<ProfileFollowingAuthorModel> FollowingAuthors { get; set; } = [];
    public ICollection<PlaylistListModel> CreatedPlaylists { get; set; } = [];

    public static ProfileDetailModel Empty => new ProfileDetailModel
    {
        Id = Guid.Empty,
        Name = string.Empty,
        Following = [],
        Followers = [],
        CreatedPlaylists = [],
    };
}