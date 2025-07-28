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

    public void NavigateToYourShows()
    {
        NavigationRequested?.Invoke(new NavigateToYourShowsRequest());
    }
    
    public void NavigateToMadeForYou()
    {
        NavigationRequested?.Invoke(new NavigateToMadeForYouRequest());
    }

    public void NavigateToRecentlyPlayed()
    {
        NavigationRequested?.Invoke(new NavigateToRecentlyPlayedRequest());
    }
    public void NavigateToPopularRadio()
    {
        NavigationRequested?.Invoke(new NavigateToPopularRadioRequest());
    }

}