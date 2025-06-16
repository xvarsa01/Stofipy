using System.ComponentModel;
using Stofipy.BL.Models;

namespace Stofipy.App.Services.Interfaces;

public interface ICurrentStateService : INotifyPropertyChanged
{
    FilesInQueueModel? NowPlaying { get; set; }
    bool IsPlaying { get; set; }
}