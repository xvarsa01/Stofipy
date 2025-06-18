using Stofipy.App.ViewModels;
using Stofipy.App.ViewModels.Author;

namespace Stofipy.App.Services;

public abstract class NavigationRequest { }

public class NavigateToHomeRequest : NavigationRequest { }
public class NavigateToYourShowsRequest : NavigationRequest { }
public class NavigateToMadeForYouRequest : NavigationRequest { }
public class NavigateToRecentlyPlayedRequest : NavigationRequest { }
public class NavigateToPopularRadioRequest : NavigationRequest { }

public class NavigateToAuthorRequest(Guid authorId) : NavigationRequest
{
    public Guid AuthorId { get; } = authorId;
}

public class NavigateToPlaylistRequest(Guid playlistId) : NavigationRequest
{
    public Guid PlaylistId { get; } = playlistId;
}
public class NavigateToAlbumRequest(Guid albumId) : NavigationRequest
{
    public Guid AlbumId { get; } = albumId;
}