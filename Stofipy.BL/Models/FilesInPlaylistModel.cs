using Stofipy.BL.Models.InterfaceBase;

namespace Stofipy.BL.Models;

public record FilesInPlaylistModel() : ModelBase
{
    public required Guid FileId { get; set; }
    public required string FileName { get; set; }
    public required string AuthorName { get; set; }
    public string? DefaultAlbumName { get; set; }
    public string? Picture { get; set; }
    public required int Length { get; set; }
    public string LengthFormatted => $"{Length / 60}:{Length % 60:D2}";
    public required int IndexActual { get; set; }
    public required int IndexCustom { get; set; }

    public bool IsHovered { get; set; }
    public bool IsSelected { get; set; }
    public bool IsSelectedOrHovered =>IsSelected || IsHovered;

    public static FilesInPlaylistModel Empty = new()
    {
        Id = Guid.Empty,
        FileName = string.Empty,
        AuthorName = string.Empty,
        DefaultAlbumName = string.Empty,
        Length = 0,
        FileId = Guid.Empty,
        IndexActual = 0,
        IndexCustom = 0,
    };
}