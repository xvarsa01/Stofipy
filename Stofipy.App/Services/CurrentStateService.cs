using CommunityToolkit.Mvvm.ComponentModel;
using Stofipy.App.Enums;
using Stofipy.App.Messages;
using Stofipy.App.Services.Interfaces;
using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Models;

namespace Stofipy.App.Services;

public partial class CurrentStateService(
    IFilesInQueueFacade filesInQueueFacade,
    IMessengerService messengerService)
    : ObservableObject, ICurrentStateService
{
    [ObservableProperty]
    private FilesInQueueModel? _nowPlaying;
    
    public PlayingSourceType NowPlayingSource { get; set; }
    public bool IsPaused { get; set; }
    public bool IsSomethingPlaying => NowPlayingSource != PlayingSourceType.Unknown;
    public Guid CurrentlyPlayingPlaylistId { get; set; }
    public Guid CurrentlyPlayingAuthorId { get; set; }
    public Guid CurrentlyPlayingAlbumId { get; set; }

    public async Task PlayPlaylist(Guid playlistId)
    {
        messengerService.Send(new PlayPauseButtonClickedMessage());
        if (NowPlayingSource == PlayingSourceType.Playlist && CurrentlyPlayingPlaylistId == playlistId)
        {
            IsPaused = !IsPaused;
            return;
        }
        await filesInQueueFacade.AddPlaylistToQueue(playlistId, false);
        messengerService.Send(new RefreshQueueMessage());

        IsPaused = false;
        NowPlayingSource = PlayingSourceType.Playlist;
        CurrentlyPlayingPlaylistId = playlistId;
    }
    
    public async Task PlayAuthor(Guid authorId)
    {
        messengerService.Send(new PlayPauseButtonClickedMessage());
        if (NowPlayingSource == PlayingSourceType.Author && CurrentlyPlayingAuthorId == authorId)
        {
            IsPaused = !IsPaused;
            return;
        }
        await filesInQueueFacade.AddAuthorToQueue(authorId);
        messengerService.Send(new RefreshQueueMessage());

        IsPaused = false;
        NowPlayingSource = PlayingSourceType.Author;
        CurrentlyPlayingAuthorId = authorId;
    }

    public async Task PlayAlbum(Guid albumId)
    {
        messengerService.Send(new PlayPauseButtonClickedMessage());
        if (NowPlayingSource == PlayingSourceType.Album && CurrentlyPlayingAlbumId == albumId)
        {
            IsPaused = !IsPaused;
            return;
        }
        await filesInQueueFacade.AddAlbumToQueue(albumId, false);;
        messengerService.Send(new RefreshQueueMessage());

        IsPaused = false;
        NowPlayingSource = PlayingSourceType.Album;
        CurrentlyPlayingAlbumId = albumId;
    }
}