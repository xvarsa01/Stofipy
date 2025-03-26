using Stofipy.BL.Models.InterfaceBase;

namespace Stofipy.BL.Models;

public record AlbumListModel() : ModelBase
{
    public required string AlbumName { get; set; }
    public string? Picture { get; set; }
    public int? Year { get; set; }
    
    public required Guid AuthorId { get; set; }
    public required string AuthorName { get; set; }
    
    public static AlbumListModel Empty = new()
    {
        AlbumName = string.Empty,
        AuthorId = Guid.Empty,
        AuthorName = string.Empty,
        Id = Guid.NewGuid()
    };
}