using System.ComponentModel;
using Stofipy.App.Enums;
using Stofipy.BL.Models;

namespace Stofipy.App.Services.Interfaces;

public interface ICurrentStateService : INotifyPropertyChanged
{
    FilesInQueueModel? NowPlaying { get; set; }
    PlayingSourceType NowPlayingSource { get; set; }
    bool IsPaused { get; set; }
    bool IsSomethingPlaying { get; }
    Guid CurrentlyPlayingItemId { get; set; }
    Guid CurrentlyPlayingPlaylistId { get; set; }
    Guid CurrentlyPlayingAuthorId { get; set; }
    Guid CurrentlyPlayingAlbumId { get; set; }

    Task PlayPlaylist(Guid playlistId);
    Task PlayAuthor(Guid authorId);
    Task PlayAlbum(Guid albumId);
}