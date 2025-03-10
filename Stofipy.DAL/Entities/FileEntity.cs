namespace Stofipy.DAL.Entities;

public record FileEntity : IEntity
{
    public required Guid Id { get; set; }
    public required string FileName { get; set; }
    
    public required Guid AuthorId { get; set; }
    public required AuthorEntity Author { get; set; }
    public ICollection<FilesInPlaylists> FilesInPlaylists { get; set; } = [];

}