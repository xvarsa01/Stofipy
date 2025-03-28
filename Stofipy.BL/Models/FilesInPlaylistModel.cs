using Stofipy.BL.Models.InterfaceBase;

namespace Stofipy.BL.Models;

public record FilesInPlaylistModel() : ModelBase
{
    public required Guid FileId { get; set; }
    public required string FileName { get; set; }
    public string? DefaultAlbumName { get; set; }
    public required int IndexActual { get; set; }
    public required int IndexCustom { get; set; }
    
    public static FilesInPlaylistModel Empty = new()
    {
        Id = Guid.Empty,
        FileName = string.Empty,
        DefaultAlbumName = string.Empty,
        FileId = Guid.Empty,
        IndexActual = 0,
        IndexCustom = 0,
    };
}