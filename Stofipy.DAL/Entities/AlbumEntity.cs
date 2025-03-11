namespace Stofipy.DAL.Entities;

public record AlbumEntity : IEntity
{
    public required Guid Id { get; set; }
    public required string AlbumName { get; set; }
    public required string Description { get; set; }
    
    public required Guid AuthorId { get; set; }
    public required AuthorEntity Author { get; set; }
    public ICollection<FilesInAlbumEntity> FilesInAlbums { get; set; } = [];
}