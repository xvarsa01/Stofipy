namespace Stofipy.DAL.Entities;

public record PlaylistEntity : IEntity
{
    public required Guid Id { get; set; }
    public required string PlaylistName { get; set; }
    public required string Description { get; set; }
    public string? Picture { get; set; }
    public required int PlayCount { get; set; }
    public required bool IsPublic { get; set; }

    public ICollection<FilesInPlaylistEntity> FilesInPlaylists { get; set; } = [];
    public required ProfileEntity CreatedBy { get; set; }
    public required Guid CreatedById { get; set; }
}