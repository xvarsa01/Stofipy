using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.Input;
using Stofipy.App.Enums;
using Stofipy.App.Messages;
using Stofipy.App.Services;
using Stofipy.App.Services.Interfaces;
using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Models;

namespace Stofipy.App.ViewModels.Album;

public partial class AlbumDetailVM (
    IAlbumFacade albumFacade,
    IAuthorFacade authorFacade,
    IFileFacade fileFacade,
    IFilesInAlbumFacade filesInAlbumFacade,
    IFilesInQueueFacade filesInQueueFacade,
    INavigationService navigationService,
    ICurrentStateService currentState,
    IMessengerService messengerService)
    : ViewModelBase(messengerService)
{
    private Guid Id { get; set; }
    public AlbumDetailModel Album { get; set; } = null!;
    
    private FilesInAlbumModel? SelectedFile {get; set; }
    public ObservableCollection<FilesInAlbumModel> Files { get; set; } = [];
    public ObservableCollection<AlbumListModel> MoreAlbumsFromAuthor { get; set; } = [];
    
    public bool ThisAlbumIsPlaying => currentState.NowPlayingSource == PlayingSourceType.Album 
                                         && currentState.CurrentlyPlayingAlbumId == Album.Id
                                         && !currentState.IsPaused;
    public async Task LoadByIdAsync(Guid id)
    {
        Id = id;
        await LoadDataAsync();
    }
    
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Album = await albumFacade.GetByIdAsync(Id) ?? AlbumDetailModel.Empty;
        var author = await authorFacade.GetByIdAsync(Album.AuthorId);
        MoreAlbumsFromAuthor = author?.Albums.Where(x => x.Id != Id).Take(5).ToObservableCollection() ?? [];
        Files = (await filesInAlbumFacade.GetAllByAlbumIdAsync(Id)).ToObservableCollection();
    }
    [RelayCommand]
    private async Task PlayAlbumAsync()
    {
        await currentState.PlayAlbum(Id);
        OnPropertyChanged(nameof(ThisAlbumIsPlaying));
    }
    
    [RelayCommand]
    private Task PlayItemAsync(FilesInAlbumModel item)
    {
        return Task.CompletedTask;
    }
    
    [RelayCommand]
    private async Task GoToAuthor(FilesInAlbumModel filesInAlbum)
    {
        var file = await fileFacade.GetByIdAsync(filesInAlbum.FileId);
        if (file != null) navigationService.NavigateToAuthor(file.AuthorId);
    }
    
    [RelayCommand]
    private async Task PlayOtherAlbumFromAuthor(Guid id)
    {
        await currentState.PlayAlbum(id);
    }
    
    [RelayCommand]
    private void GoToOtherAlbumFromAuthor(Guid id)
    {
        navigationService.NavigateToAlbum(id);
    }
    
    [RelayCommand]
    private void SelectRow(FilesInAlbumModel item)
    {
        if (SelectedFile != null)
            SelectedFile.IsSelected = false;
    
        SelectedFile = item;
        item.IsSelected = true;
    }
}