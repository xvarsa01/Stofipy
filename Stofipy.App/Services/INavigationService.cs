using Stofipy.App.ViewModels;
using Stofipy.App.ViewModels.Author;

namespace Stofipy.App.Services;

public interface INavigationService
{
    event Func<NavigationRequest, Task>? NavigationRequested;

    void NavigateToHome();
    void NavigateToPlaylist(Guid playlistId);
    void NavigateToAuthor(Guid authorId);
    void NavigateToAlbum(Guid albumId);

}