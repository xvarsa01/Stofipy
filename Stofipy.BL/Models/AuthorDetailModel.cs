using Stofipy.BL.Models.InterfaceBase;

namespace Stofipy.BL.Models;

public record AuthorDetailModel() : ModelBase
{
    public required string AuthorName { get; set; }
    public string? ProfilePicture { get; set; }
    
    public ICollection<FileListModel> Files { get; set; } = [];
    public ICollection<AlbumListModel> Albums { get; set; } = [];
    
    public static AuthorDetailModel Empty = new()
    {
        AuthorName = string.Empty,
        Id = Guid.Empty
    };
}