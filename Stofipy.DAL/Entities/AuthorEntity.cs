namespace Stofipy.DAL.Entities;

public record AuthorEntity : IEntity
{
    public required Guid Id { get; set; }
    public required string AuthorName { get; set; }

    public ICollection<FileEntity> Files { get; set; } = [];
}