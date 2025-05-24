using Stofipy.App.ViewModels;
using Stofipy.App.ViewModels.Author;

namespace Stofipy.App.Services;

public abstract class NavigationRequest { }

public class NavigateToHomeRequest : NavigationRequest { }

public class NavigateToAuthorRequest(Guid authorId) : NavigationRequest
{
    public Guid AuthorId { get; } = authorId;
}

public class NavigateToPlaylistRequest(Guid playlistId) : NavigationRequest
{
    public Guid PlaylistId { get; } = playlistId;
}