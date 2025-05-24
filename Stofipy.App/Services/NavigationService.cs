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
}