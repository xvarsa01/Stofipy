using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Models;

namespace Stofipy.App.ViewModels;

public partial class PlaylistDetailVM (IPlaylistFacade playlistFacade, IFilesInPlaylistFacade filesInPlaylistFacade ,IFilesInQueueFacade filesInQueueFacade) : ViewModelBase
{
    private Guid Id { get; set; }
    public PlaylistDetailModel Playlist { get; set; } = null!;
    
    private FilesInPlaylistModel? SelectedFile {get; set; }
    public ObservableCollection<FilesInPlaylistModel> Files { get; set; } = [];
    
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
        await filesInQueueFacade.AddPlaylistToQueue(Playlist.Id, false);
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