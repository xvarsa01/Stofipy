namespace Stofipy.DAL.Entities;

public record FilesInPlaylistEntity : IEntity
{
    public required Guid Id { get; set; }
    public required Guid PlaylistId { get; set; } 
    public required Guid FileId { get; set; }
    public required PlaylistEntity Playlist { get; set; }
    public required FileEntity File { get; set; }
    public required int IndexActual { get; set; }
    public required int IndexCustom { get; set; }
}