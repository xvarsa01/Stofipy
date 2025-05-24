using Stofipy.App.ViewModels;
using Stofipy.App.ViewModels.Author;

namespace Stofipy.App.Services;

public abstract class NavigationRequest { }

public class NavigateToHomeRequest : NavigationRequest { }

public class NavigateToAuthorRequest : NavigationRequest
{
    public AuthorDetailVM ViewModel { get; }

    public NavigateToAuthorRequest(AuthorDetailVM vm)
    {
        ViewModel = vm;
    }
}

public class NavigateToPlaylistRequest : NavigationRequest
{
    public PlaylistDetailVM ViewModel { get; }

    public NavigateToPlaylistRequest(PlaylistDetailVM vm)
    {
        ViewModel = vm;
    }
}