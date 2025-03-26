using Stofipy.BL.Models.InterfaceBase;

namespace Stofipy.BL.Models;

public record PlaylistListModel() : ModelBase
{
    public required string PlaylistName { get; set; }
    public string? Picture { get; set; }

    public List<string> Authors { get; set; } = [];
    
    public static PlaylistListModel Empty = new()
    {
        PlaylistName = string.Empty,
        Id = Guid.Empty
    };
}