using Stofipy.App.ViewModels;
using Stofipy.App.ViewModels.Author;
using Stofipy.App.Views.MainPages;

namespace Stofipy.App.Services;

public class NavigationService : INavigationService
{
    private readonly ListOfPlaylistsVM _listOfPlaylistsVM;
    private readonly PlaylistDetailVM _playlistDetailVM;
    
    private readonly AuthorDetailVM _authorDetailVM;
    private readonly FilesInQueueVM _filesInQueueVM;

    public NavigationService(ListOfPlaylistsVM listOfPlaylistsVM, FilesInQueueVM filesInQueueVM, PlaylistDetailVM playlistDetailVM, AuthorDetailVM authorDetailVM)
    {
        _listOfPlaylistsVM = listOfPlaylistsVM;
        _playlistDetailVM = playlistDetailVM;
        _authorDetailVM = authorDetailVM;
        _filesInQueueVM = filesInQueueVM;
    }

    public event Action<NavigationRequest>? NavigationRequested;

    public void NavigateToHome()
    {
        NavigationRequested?.Invoke(new NavigateToHomeRequest());
    }

    public void NavigateToPlaylist(PlaylistDetailVM vm)
    {
        NavigationRequested?.Invoke(new NavigateToPlaylistRequest(vm));
    }

    public void NavigateToAuthor(AuthorDetailVM vm)
    {
        NavigationRequested?.Invoke(new NavigateToAuthorRequest(vm));
    }
}