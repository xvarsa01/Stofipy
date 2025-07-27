using Stofipy.BL.Models.InterfaceBase;

namespace Stofipy.BL.Models;

public record PlaylistDetailModel() : ModelBase
{
    public required string PlaylistName { get; set; }
    public required string Description { get; set; }
    public string? Picture { get; set; }
    public required bool IsPublic {get; set; }
    public string PublicStatus => IsPublic ? "Public Playlist" : "Private Playlist";
    public required int Length { get; set; }
    public string LengthFormatted => $"{Length / 60 / 60}hr{Length / 60}min";
    public required int PlayCount { get; set; }
    
    public required string CreatedByName { get; set; }
    public required Guid CreatedById { get; set; }

    
    public static PlaylistDetailModel Empty = new()
    {
        PlaylistName = string.Empty,
        Description = string.Empty,
        Id = Guid.Empty,
        Length = 0,
        PlayCount = 0,
        IsPublic = false,
        CreatedByName = string.Empty,
        CreatedById = Guid.Empty,
    };
}