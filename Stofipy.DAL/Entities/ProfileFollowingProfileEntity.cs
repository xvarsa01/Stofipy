namespace Stofipy.DAL.Entities;

public record ProfileFollowingProfileEntity : IEntity
{
    public required Guid Id { get; set; }
    public required Guid FollowerId { get; set; }
    public required Guid FollowedId { get; set; }
    public required ProfileEntity Follower { get; set; }
    public required ProfileEntity Followed { get; set; }
}