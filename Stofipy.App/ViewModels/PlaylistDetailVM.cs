using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.Input;
using Stofipy.App.Messages;
using Stofipy.App.Services.Interfaces;
using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Models;

namespace Stofipy.App.ViewModels;

public partial class PlaylistDetailVM (
    IPlaylistFacade playlistFacade,
    IFilesInPlaylistFacade filesInPlaylistFacade,
    IFilesInQueueFacade filesInQueueFacade,
    ICurrentStateService currentState,
    IMessengerService messengerService)
    : ViewModelWithCurrentState(currentState, messengerService)
{
    private Guid Id { get; set; }
    public PlaylistDetailModel Playlist { get; set; } = null!;
    
    private FilesInPlaylistModel? SelectedFile {get; set; }
    public ObservableCollection<FilesInPlaylistModel> Files { get; set; } = [];
    
    public bool ThisPlaylistIsPlaying
    {
        get => IsPlaylistPlaying && CurrentlyPlayingPlaylistId == Playlist.Id;
        set
        {
            if (value){
                IsPlaylistPlaying = true; CurrentlyPlayingPlaylistId = Playlist.Id;
            }
            else
            {
                IsPlaylistPlaying = false;
            }
        }
    }

    public async Task LoadByIdAsync(Guid id)
    {
        Id = id;
        await LoadDataAsync();
    }
    
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Playlist = await playlistFacade.GetByIdAsync(Id) ?? PlaylistDetailModel.Empty;
        Files = (await filesInPlaylistFacade.GetAllAsync(Id, 1, 10)).ToObservableCollection();
    }
    [RelayCommand]
    private async Task PlayPlaylistAsync()
    {
        MessengerService.Send(new PlayPauseButtonClickedMessage());
        if (ThisPlaylistIsPlaying)
        {
            ThisPlaylistIsPlaying = false;
            return;
        }
        
        if (NowPlaying == null || CurrentlyPlayingPlaylistId != Playlist.Id)
        {
            await filesInQueueFacade.AddPlaylistToQueue(Playlist.Id, false);
            MessengerService.Send(new RefreshQueueMessage());
        }
        ThisPlaylistIsPlaying = true;
        OnPropertyChanged(nameof(ThisPlaylistIsPlaying));
    }
    
    [RelayCommand]
    private Task PlayItemAsync(FilesInPlaylistModel item)
    {
        return Task.CompletedTask;
    }
    
    [RelayCommand]
    private Task SelectRowAsync(FilesInPlaylistModel item)
    {
        if (SelectedFile != null)
            SelectedFile.IsSelected = false;
    
        SelectedFile = item;
        item.IsSelected = true;
        return Task.CompletedTask;
    }
}