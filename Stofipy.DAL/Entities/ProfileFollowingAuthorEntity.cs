namespace Stofipy.DAL.Entities;

public record ProfileFollowingAuthorEntity : IEntity
{
    public required Guid Id { get; set; }
    public required Guid FollowerId { get; set; }
    public required Guid AuthorId { get; set; }
    public required ProfileEntity Follower { get; set; }
    public required AuthorEntity Author { get; set; }
}