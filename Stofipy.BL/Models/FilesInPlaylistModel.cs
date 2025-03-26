using Stofipy.BL.Models.InterfaceBase;

namespace Stofipy.BL.Models;

public record FilesInPlaylistModel() : ModelBase
{
    public required Guid FileId { get; set; }
    public required string FileName { get; set; }
    public required int Index { get; set; }
    
    public static FilesInPlaylistModel Empty = new()
    {
        Id = Guid.Empty,
        FileName = string.Empty,
        FileId = Guid.Empty,
        Index = 0,
    };
}