namespace Stofipy.DAL.Entities;

public record FilesInQueueEntity() : IEntity
{
    public required Guid Id { get; set; }
    public required int Index { get; set; }
    public required bool PriorityQueue { get; set; }
    public required FileEntity File { get; set; }
    public required Guid FileId { get; set; }
}