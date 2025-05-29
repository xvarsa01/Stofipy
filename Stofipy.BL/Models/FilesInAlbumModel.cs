using Stofipy.BL.Models.InterfaceBase;

namespace Stofipy.BL.Models;

public record FilesInAlbumModel() : ModelBase
{
    public required Guid FileId { get; set; }
    public required string FileName { get; set; }
    public required int PlayCount { get; set; }
    public required int Length { get; set; }
    public string LengthFormatted => $"{Length / 60}:{Length % 60:D2}";
    public required int Index { get; set; }
    public bool IsHovered { get; set; }
    public bool IsSelected { get; set; }
    
    public static FilesInAlbumModel Empty = new()
    {
        Id = Guid.Empty,
        FileName = string.Empty,
        PlayCount = 0,
        Length = 0,
        FileId = Guid.Empty,
        Index = 0,
    };
    
}