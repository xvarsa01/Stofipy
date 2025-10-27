using System.ComponentModel;
using Stofipy.BL.Models;

namespace Stofipy.App.Services.Interfaces;

public interface ICurrentStateService : INotifyPropertyChanged
{
    FilesInQueueModel? NowPlaying { get; set; }
    bool IsSomethingPlaying { get; }
    
    bool IsPlaylistPlaying { get; set; }
    Guid CurrentlyPlayingPlaylistId { get; set; }
    bool IsAuthorPlaying { get; set; }
    bool IsAlbumPlaying { get; set; }
    bool IsSomethingElsePlaying { get; set; }
}