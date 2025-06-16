using CommunityToolkit.Mvvm.ComponentModel;
using Stofipy.App.Services.Interfaces;
using Stofipy.BL.Models;

namespace Stofipy.App.Services;

public partial class CurrentStateService : ObservableObject, ICurrentStateService
{
    [ObservableProperty]
    private FilesInQueueModel? _nowPlaying;
    
    [ObservableProperty]
    private bool _isPlaying;

}