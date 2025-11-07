using Stofipy.App.Enums;
using Stofipy.App.Services.Interfaces;
using Stofipy.BL.Models;

namespace Stofipy.App.ViewModels;

public class ViewModelWithCurrentState(ICurrentStateService currentState, IMessengerService messengerService)
    : ViewModelBase(messengerService)
{
    protected FilesInQueueModel? NowPlaying => currentState.NowPlaying;

    protected Guid CurrentlyPlayingPlaylistId
    {
        get => currentState.CurrentlyPlayingPlaylistId;
        set => currentState.CurrentlyPlayingPlaylistId = value;
    }
}