namespace Stofipy.DAL.Entities;

public record FilesInAlbumEntity : IEntity
{
    public required Guid Id { get; set; }
    public required Guid AlbumId { get; set; }
    public required Guid FileId { get; set; }
    public required AlbumEntity Album { get; set; }
    public required FileEntity File { get; set; }
    public required int Index { get; set; }
}