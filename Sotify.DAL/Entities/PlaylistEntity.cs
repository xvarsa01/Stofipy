namespace Sotify.DAL.Entities;

public record PlaylistEntity : IEntity
{
    public required Guid Id { get; set; }
    public required string PlaylistName { get; set; }

    public ICollection<FilesInPlaylists> FilesInPlaylists { get; set; } = [];
}