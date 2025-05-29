using Stofipy.BL.Models.InterfaceBase;
using Stofipy.DAL.Enums;

namespace Stofipy.BL.Models;

public record FileDetailModel() : ModelBase
{
    public required string FileName { get; set; }
    public required string Description { get; set; }
    public string? Picture { get; set; }
    public string? Lyrics { get; set; }
    public required double Size { get; set; }
    public required int Length { get; set; }
    public required int PlayCount { get; set; }
    public required Category Category { get; set; }
    
    public required Guid AuthorId { get; set; }
    public required string AuthorName { get; set; }
    public Guid? DefaultAlbumId { get; set; }
    public string? DefaultAlbumName { get; set; }

    public static FileDetailModel Empty = new()
    {
        Id = Guid.Empty,
        FileName = string.Empty,
        Description = string.Empty,
        Picture = null,
        Lyrics = null,
        Size = 0,
        Length = 0,
        PlayCount = 0,
        Category = Category.Rock,
        AuthorId = default,
        AuthorName = string.Empty,
        DefaultAlbumId = null,
        DefaultAlbumName = null,
    };
}