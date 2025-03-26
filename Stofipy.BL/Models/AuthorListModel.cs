using Stofipy.BL.Models.InterfaceBase;

namespace Stofipy.BL.Models;

public record AuthorListModel : ModelBase
{
    public required string AuthorName { get; set; }
    public string? ProfilePicture { get; set; }
    
    public static AuthorListModel Empty = new()
    {
        AuthorName = string.Empty,
        Id = Guid.Empty
    };
    
}