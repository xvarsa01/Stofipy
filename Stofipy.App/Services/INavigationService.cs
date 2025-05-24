using Stofipy.App.ViewModels;
using Stofipy.App.ViewModels.Author;

namespace Stofipy.App.Services;

public interface INavigationService
{
    event Action<NavigationRequest>? NavigationRequested;

    void NavigateToHome();
    void NavigateToPlaylist(PlaylistDetailVM vm);
    void NavigateToAuthor(AuthorDetailVM vm);

}