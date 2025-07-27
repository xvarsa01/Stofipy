using Stofipy.BL.Models.InterfaceBase;

namespace Stofipy.BL.Models;

public record ProfileListModel : ModelBase
{
    public required string Name { get; set; }
    public string? ProfilePicture { get; set; }

    public static ProfileListModel Empty => new()
    {
        Id = Guid.Empty,
        Name = string.Empty,
        ProfilePicture = null
    };
}