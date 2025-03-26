using Stofipy.BL.Models.InterfaceBase;

namespace Stofipy.BL.Models;

public record PlaylistDetailModel() : ModelBase
{
    public required string PlaylistName { get; set; }
    public required string Description { get; set; }
    public string? Picture { get; set; }
    public required int Length { get; set; }

    public ICollection<FilesInPlaylistModel> FilesInPlaylists { get; set; } = [];
    
    public static PlaylistDetailModel Empty = new()
    {
        PlaylistName = string.Empty,
        Description = string.Empty,
        Id = Guid.Empty,
        Length = 0
    };
}