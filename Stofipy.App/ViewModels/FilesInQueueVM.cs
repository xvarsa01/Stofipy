using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.Input;
using Stofipy.App.Views;
using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Models;

namespace Stofipy.App.ViewModels;

public partial class FilesInQueueVM(IFilesInQueueFacade facade) : ViewModelBase
{
    public ObservableCollection<FilesInQueueModel> PriorityQueue { get; set; } = null!;
    public ObservableCollection<FilesInQueueModel> BasicQueue { get; set; } = null!;
    public ObservableCollection<FilesInQueueModel> RecentlyPlayedQueue { get; set; } = null!;

    public FilesInQueueModel? NowPlaying { get; set; }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        var current = await facade.GetCurrentAsync();
        var priority = await facade.GetAllPriorityFilesInQueueAsync();
        var basic = await facade.GetAllNonPriorityFilesInQueueAsync();
        var recent = await facade.GetRecentFilesInQueueAsync(20);
        
        NowPlaying = current;
        PriorityQueue = priority.ToObservableCollection();
        BasicQueue = basic.ToObservableCollection();
        RecentlyPlayedQueue = recent.ToObservableCollection();
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
    
    [RelayCommand]
    private async Task NextSong()
    {
        await facade.NextSong();
        
        await base.LoadDataAsync();
        var current = await facade.GetCurrentAsync();
        var priority = await facade.GetAllPriorityFilesInQueueAsync();
        var basic = await facade.GetAllNonPriorityFilesInQueueAsync();
        
        NowPlaying = current;
        PriorityQueue = priority.ToObservableCollection();
        BasicQueue = basic.ToObservableCollection();
    }
    
    [RelayCommand]
    private async Task PreviousSong()
    {
        await facade.PreviousSong();
        
        await base.LoadDataAsync();
        var current = await facade.GetCurrentAsync();
        var priority = await facade.GetAllPriorityFilesInQueueAsync();
        var basic = await facade.GetAllNonPriorityFilesInQueueAsync();
        
        NowPlaying = current;
        PriorityQueue = priority.ToObservableCollection();
        BasicQueue = basic.ToObservableCollection();
    }
}