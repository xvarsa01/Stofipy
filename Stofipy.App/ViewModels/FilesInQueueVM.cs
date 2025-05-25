using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Stofipy.App.Messages;
using Stofipy.App.Services.Interfaces;
using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Models;

namespace Stofipy.App.ViewModels;

public partial class FilesInQueueVM(
    IFilesInQueueFacade facade,
    IMessengerService messengerService)
    : ViewModelBase(messengerService), IRecipient<RefreshQueueMessage>
{
    public ObservableCollection<FilesInQueueModel> PriorityQueue { get; set; } = null!;
    public ObservableCollection<FilesInQueueModel> BasicQueue { get; set; } = null!;
    public ObservableCollection<FilesInQueueModel> RecentlyPlayedQueue { get; set; } = null!;

    public FilesInQueueModel? NowPlaying { get; set; }
    
    [ObservableProperty]
    private bool _displayStandardQueue = true;
    [ObservableProperty]
    private bool _displayRecentlyPlayed;
    
    [ObservableProperty]
    private bool _displayPriorityQueue = true;
    
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        NowPlaying = await facade.GetCurrentAsync();
        PriorityQueue = (await facade.GetAllPriorityFilesInQueueAsync()).ToObservableCollection();
        BasicQueue = (await facade.GetAllNonPriorityFilesInQueueAsync()).ToObservableCollection();
        RecentlyPlayedQueue = (await facade.GetRecentFilesInQueueAsync(20)).ToObservableCollection();
        
        DisplayPriorityQueue = PriorityQueue.Count != 0;
    }


    [RelayCommand]
    private void ShowQueue()
    {
        DisplayStandardQueue = true;
        DisplayRecentlyPlayed = false;
    }

    [RelayCommand]
    private void ShowRecentlyPlayed()
    {
        DisplayStandardQueue = false;
        DisplayRecentlyPlayed = true;
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

        NowPlaying = await facade.GetCurrentAsync();
        PriorityQueue = (await facade.GetAllPriorityFilesInQueueAsync()).ToObservableCollection();
        BasicQueue = (await facade.GetAllNonPriorityFilesInQueueAsync()).ToObservableCollection();
        RecentlyPlayedQueue = (await facade.GetRecentFilesInQueueAsync(20)).ToObservableCollection();
        
        DisplayPriorityQueue = PriorityQueue.Count != 0;
    }
    
    [RelayCommand]
    private async Task PreviousSong()
    {
        await facade.PreviousSong();
        
        await base.LoadDataAsync();

        NowPlaying = await facade.GetCurrentAsync();
        PriorityQueue = (await facade.GetAllPriorityFilesInQueueAsync()).ToObservableCollection();
        BasicQueue = (await facade.GetAllNonPriorityFilesInQueueAsync()).ToObservableCollection();
        RecentlyPlayedQueue = (await facade.GetRecentFilesInQueueAsync(20)).ToObservableCollection();
        
        DisplayPriorityQueue = PriorityQueue.Count != 0;
    }
}