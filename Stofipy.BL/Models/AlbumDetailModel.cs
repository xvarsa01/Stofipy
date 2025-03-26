using Stofipy.BL.Models.InterfaceBase;

namespace Stofipy.BL.Models;

public record AlbumDetailModel() : ModelBase
{
    public required string AlbumName { get; set; }
    public required string Description { get; set; }
    public string? Picture { get; set; }
    public int? Year { get; set; }
    public required int Length { get; set; }
    
    public required Guid AuthorId { get; set; }
    public required string AuthorName { get; set; }
    public List<FilesInAlbumModel> FilesInAlbums { get; set; } = [];
    
    public static AlbumDetailModel Empty = new()
    {
        AlbumName = string.Empty,
        Description = string.Empty,
        Length = 0,
        AuthorId = Guid.Empty,
        AuthorName = string.Empty,
        Id = Guid.NewGuid()
    };
}