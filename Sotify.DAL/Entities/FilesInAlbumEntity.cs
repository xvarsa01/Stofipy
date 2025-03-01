namespace Sotify.DAL.Entities;

public record FilesInAlbumEntity : IEntity
{
    public Guid Id { get; set; }
    public required Guid AlbumId { get; set; }
    public required Guid FileId { get; set; }
    public required AlbumEntity Album { get; set; }
    public required FileEntity File { get; set; }
}