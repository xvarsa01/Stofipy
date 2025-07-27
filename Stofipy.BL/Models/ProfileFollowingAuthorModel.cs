using Stofipy.BL.Models.InterfaceBase;

namespace Stofipy.BL.Models;

public record ProfileFollowingAuthorModel : ModelBase
{
    public required Guid FollowerId { get; set; }
    public required Guid AuthorId { get; set; }

    public static ProfileFollowingAuthorModel Empty => new()
    {
        Id = Guid.Empty,
        FollowerId = Guid.Empty,
        AuthorId = Guid.Empty
    };
}