using Stofipy.App.ViewModels;
using Stofipy.App.ViewModels.Author;
using Stofipy.App.Views.MainPages;

namespace Stofipy.App.Services;

public class NavigationService : INavigationService
{

    public event Func<NavigationRequest, Task>? NavigationRequested;

    public void NavigateToHome()
    {
        NavigationRequested?.Invoke(new NavigateToHomeRequest());
    }

    public void NavigateToPlaylist(Guid playlistId)
    {
        NavigationRequested?.Invoke(new NavigateToPlaylistRequest(playlistId));
    }

    public void NavigateToAuthor(Guid authorId)
    {
        NavigationRequested?.Invoke(new NavigateToAuthorRequest(authorId));
    }

    public void NavigateToAlbum(Guid albumId)
    {
        NavigationRequested?.Invoke(new NavigateToAlbumRequest(albumId));
    }
}