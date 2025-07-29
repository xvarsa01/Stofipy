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
    IMessengerService messengerService,
    ICurrentStateService currentState)
    : ViewModelBase(messengerService), IRecipient<RefreshQueueMessage>
{
    public ObservableCollection<FilesInQueueModel> PriorityQueue { get; set; } = null!;
    public ObservableCollection<FilesInQueueModel> BasicQueue { get; set; } = null!;
    public ObservableCollection<FilesInQueueModel> RecentlyPlayedQueue { get; set; } = null!;

    public FilesInQueueModel? NowPlaying
    {
        get => currentState.NowPlaying;
        set => currentState.NowPlaying = value;
    }
    
    [ObservableProperty]
    private bool _displayStandardQueue = true;
    [ObservableProperty]
    private bool _displayRecentlyPlayed;
    
    [ObservableProperty]
    private bool _displayPriorityQueue = true;
    
    private FilesInQueueModel? _draggedFile = null;
    [ObservableProperty]
    private bool _isInputTransparent = true;
    
    [ObservableProperty]
    private bool _draggedIntoLastNonPriority = false;
    [ObservableProperty]
    private bool _draggedIntoLastPriority = false;
    
    private FilesInQueueModel? SelectedFile {get; set; }

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
    public void DragStarted(FilesInQueueModel draggedFile)
    {
        _draggedFile = draggedFile;
        IsInputTransparent = false;
    }

    [RelayCommand]
    public void DragReleased()
    {
        foreach (var file in BasicQueue)
        {
            file.IsDraggedInto = false;
        }
        DraggedIntoLastPriority = false;
        DraggedIntoLastNonPriority = false;
        _draggedFile = null;
        IsInputTransparent = true;
    }
    
    [RelayCommand]
    public async Task DragEnded(FilesInQueueModel endFile)
    {
        await facade.ReorderQueue(_draggedFile!.Index, endFile.Index , _draggedFile.PriorityQueue, endFile.PriorityQueue);
        await LoadDataAsync();
    }
    [RelayCommand]
    public async Task DragEndedAtTheEndPriority()
    {
        await facade.ReorderQueue(_draggedFile!.Index, PriorityQueue.Count + 1 , _draggedFile.PriorityQueue, true);
        await LoadDataAsync();
    }
    [RelayCommand]
    public async Task DragEndedAtTheEndNonPriority()
    {
        await facade.ReorderQueue(_draggedFile!.Index, BasicQueue.Count + 1 , _draggedFile.PriorityQueue, false);
        await LoadDataAsync();
    }
    
    // just for the green highlight
    [RelayCommand]
    public void DragOver(FilesInQueueModel endFile)
    {
        if (_draggedFile == null) return;
        endFile.IsDraggedInto = true;
    }
    [RelayCommand]
    public void DragOverLeave(FilesInQueueModel endFile)
    {
        if (_draggedFile == null) return;
        endFile.IsDraggedInto = false;
    }
    
    [RelayCommand]
    public void DragOverAtTheEndPriority()
    {
        DraggedIntoLastPriority = true;
    }
    [RelayCommand]
    public void DragOverAtTheEndPriorityLeave()
    {
        DraggedIntoLastPriority = false;
    }
    
    [RelayCommand]
    public void DragOverAtTheEndNonPriority()
    {
        DraggedIntoLastNonPriority = true;
    }
    [RelayCommand]
    public void DragOverAtTheEndNonPriorityLeave()
    {
        DraggedIntoLastNonPriority = false;
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

    [RelayCommand]
    public async Task ClearQueueAsync()
    {
        await facade.RemoveAllFromQueue(true);
        
        await base.LoadDataAsync();

        NowPlaying = await facade.GetCurrentAsync();
        PriorityQueue = (await facade.GetAllPriorityFilesInQueueAsync()).ToObservableCollection();
        BasicQueue = (await facade.GetAllNonPriorityFilesInQueueAsync()).ToObservableCollection();
        RecentlyPlayedQueue = (await facade.GetRecentFilesInQueueAsync(20)).ToObservableCollection();
        
        DisplayPriorityQueue = PriorityQueue.Count != 0;
    }
    
    [RelayCommand]
    private Task SelectRowAsync(FilesInQueueModel? item)
    {
        if (SelectedFile != null)
            SelectedFile.IsSelected = false;

        item ??= NowPlaying;
        
        SelectedFile = item;
        item!.IsSelected = true;
        return Task.CompletedTask;
    }
    
    [RelayCommand]
    private Task PlayItemAsync(FilesInQueueModel item)
    {
        return Task.CompletedTask;
    }
    
    [RelayCommand]
    private Task DotsClickedAsync(FilesInQueueModel item)
    {
        return Task.CompletedTask;
    }
    
    public async void Receive(RefreshQueueMessage message)
    {
        await LoadDataAsync();
    }
}