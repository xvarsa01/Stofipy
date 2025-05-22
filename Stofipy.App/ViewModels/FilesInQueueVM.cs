using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.Input;
using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Models;

namespace Stofipy.App.ViewModels;

public partial class FilesInQueueVM(IFilesInQueueFacade facade) : ViewModelBase
{
    public ObservableCollection<FilesInQueueModel> Queue { get; set; } = null!;

    public FilesInQueueModel NowPlaying { get; set; } = null!;

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        var data = await facade.GetAllAsync();
        var nowPlaying = data.First();
        NowPlaying = nowPlaying;
        data.Remove(nowPlaying);
        Queue = data.ToObservableCollection();
    }


    [RelayCommand]
    private void ShowQueue()
    {
        
    }

    [RelayCommand]
    private void ShowRecentlyPlayed()
    {
        
    }
    
    [RelayCommand]
    private void CloseRightSection()
    {
        
    }
}