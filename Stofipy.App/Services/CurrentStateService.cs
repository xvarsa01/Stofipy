using CommunityToolkit.Mvvm.ComponentModel;
using Stofipy.App.Services.Interfaces;
using Stofipy.BL.Models;

namespace Stofipy.App.Services;

public partial class CurrentStateService : ObservableObject, ICurrentStateService
{
    [ObservableProperty]
    private FilesInQueueModel? _nowPlaying;
    
    public bool IsSomethingPlaying => NowPlaying != null;
    public bool IsPlaylistPlaying { get; set; }
    public Guid CurrentlyPlayingPlaylistId { get; set; }
    public bool IsAuthorPlaying { get; set; }
    public bool IsAlbumPlaying { get; set; }
    public bool IsSomethingElsePlaying { get; set; }
}