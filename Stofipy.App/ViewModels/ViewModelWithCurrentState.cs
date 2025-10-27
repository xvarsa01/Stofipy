using Stofipy.App.Services.Interfaces;
using Stofipy.BL.Models;

namespace Stofipy.App.ViewModels;

public class ViewModelWithCurrentState(ICurrentStateService currentState, IMessengerService messengerService)
    : ViewModelBase(messengerService)
{
    protected FilesInQueueModel? NowPlaying => currentState.NowPlaying;
    protected bool IsSomethingPlaying => currentState.IsSomethingPlaying;

    protected bool IsPlaylistPlaying
    {
        get => currentState.IsPlaylistPlaying;
        set
        {
            if (value)
            {
                currentState.IsPlaylistPlaying = true;
                currentState.IsAuthorPlaying = false;
                currentState.IsAlbumPlaying = false;
                currentState.IsSomethingElsePlaying = false;
            }
            else
            {
                currentState.IsPlaylistPlaying = false;
            }
        }
    }

    protected bool IsAuthorPlaying
    {
        get => currentState.IsAuthorPlaying;
        set
        {
            if (value)
            {
                currentState.IsPlaylistPlaying = false;
                currentState.IsAuthorPlaying = true;
                currentState.IsAlbumPlaying = false;
                currentState.IsSomethingElsePlaying = false;
            }
            else
            {
                currentState.IsAuthorPlaying = false;
            }
        }
    }    
    protected bool IsAlbumPlaying
    {
        get => currentState.IsAlbumPlaying;
        set
        {
            if (value)
            {
                currentState.IsPlaylistPlaying = false;
                currentState.IsAuthorPlaying = false;
                currentState.IsAlbumPlaying = true;
                currentState.IsSomethingElsePlaying = false;
            }
            else
            {
                currentState.IsAlbumPlaying = false;
            }
        }
    }
    protected bool IsSomethingElsePlaying
    {
        get => currentState.IsSomethingElsePlaying;
        set
        {
            if (value)
            {
                currentState.IsPlaylistPlaying = false;
                currentState.IsAuthorPlaying = false;
                currentState.IsAlbumPlaying = false;
                currentState.IsSomethingElsePlaying = true;
            }
            else
            {
                currentState.IsSomethingElsePlaying = false;
            }
        }
    }

    protected Guid CurrentlyPlayingPlaylistId
    {
        get => currentState.CurrentlyPlayingPlaylistId;
        set => currentState.CurrentlyPlayingPlaylistId = value;
    }
}