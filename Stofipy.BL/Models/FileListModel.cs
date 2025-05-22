using Stofipy.BL.Models.InterfaceBase;

namespace Stofipy.BL.Models;

public record FileListModel() : ModelBase
{
    public required string FileName { get; set; }
    public required int Length { get; set; }
    
    public required Guid AuthorId { get; set; }
    public required string AuthorName { get; set; }
    public string? Picture { get; set; }
    
    public bool IsHovered { get; set; }
    public bool IsSelected { get; set; }
    
    public static FileListModel Empty = new()
    {
        Id = Guid.Empty,
        FileName = string.Empty,
        Picture = null,
        Length = 0,
        AuthorId = default,
        AuthorName = string.Empty,
    };
}