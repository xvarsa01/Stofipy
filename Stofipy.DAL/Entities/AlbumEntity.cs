namespace Stofipy.DAL.Entities;

public record AlbumEntity : IEntity
{
    public required Guid Id { get; set; }
    public required string AlbumName { get; set; }

    public ICollection<FilesInAlbumEntity> FilesInAlbums { get; set; } = [];
}