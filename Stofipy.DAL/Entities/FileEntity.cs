using Stofipy.DAL.Enums;

namespace Stofipy.DAL.Entities;

public record FileEntity : IEntity
{
    public required Guid Id { get; set; }
    public required string FileName { get; set; }
    public required string Description { get; set; }
    public string? Lyrics { get; set; }
    public required double Size { get; set; }
    public required int Length { get; set; }
    public required Category Category { get; set; }
    
    public required Guid AuthorId { get; set; }
    public required AuthorEntity Author { get; set; }
    public ICollection<FilesInPlaylists> FilesInPlaylists { get; set; } = [];
    public ICollection<FilesInAlbumEntity> FilesInAlbums { get; set; } = [];
}