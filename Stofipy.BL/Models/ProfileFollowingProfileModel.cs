using Stofipy.BL.Models.InterfaceBase;

namespace Stofipy.BL.Models;

public record ProfileFollowingProfileModel : ModelBase
{
    public required Guid FollowerId { get; set; }
    public required Guid FollowedId { get; set; }

    public static ProfileFollowingProfileModel Empty => new()
    {
        Id = Guid.NewGuid(),
        FollowerId = Guid.Empty,
        FollowedId = Guid.Empty
    };
}