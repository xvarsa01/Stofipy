using Stofipy.BL.Models.InterfaceBase;

namespace Stofipy.BL.Models;

public record FilesInQueueModel() : ModelBase
{
    public required string FileName { get; set; }
    public required string AuthorName { get; set; }
    public string? Picture { get; set; }
    public required int Index { get; set; }
    public required Guid FileId { get; set; }
    public required bool PriorityQueue { get; set; }
    
    public bool IsDraggedInto { get; set; }
    public bool IsSelected { get; set; }
    
    
    public static FilesInQueueModel Empty = new()
    {
        Id = Guid.NewGuid(),
        FileName = string.Empty,
        AuthorName = string.Empty,
        FileId = Guid.Empty,
        Index = 0,
        PriorityQueue = false,
    };
    
}