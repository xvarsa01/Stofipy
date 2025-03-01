namespace Sotify.DAL.Entities;

public record FilesInPlaylists : IEntity
{
    public required Guid Id { get; set; }
    public required Guid PlaylistId { get; set; } 
    public required Guid FileId { get; set; }
    public required PlaylistEntity Playlist { get; set; }
    public required FileEntity File { get; set; }
}