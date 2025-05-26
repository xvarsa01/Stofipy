using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.Input;
using Stofipy.App.Services;
using Stofipy.App.Services.Interfaces;
using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Models;

namespace Stofipy.App.ViewModels;

public partial class SectionLeftVM(
    IPlaylistFacade playlistFacade,
    IAuthorFacade authorFacade,
    IAlbumFacade albumFacade,
    INavigationService navigationService,
    IMessengerService messengerService) : ViewModelBase(messengerService)
{
    public ObservableCollection<PlaylistListModel> Playlists { get; set; } = [];
    public ObservableCollection<AuthorListModel> Authors { get; set; } = [];
    public ObservableCollection<AlbumListModel> Albums { get; set; } = [];
    
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Playlists = (await playlistFacade.GetAllAsync(1, 10)).ToObservableCollection();
        Authors = (await authorFacade.GetAllAsync(1, 10)).ToObservableCollection();
        Albums = (await albumFacade.GetAllAsync(1, 10)).ToObservableCollection();
    }
    
    [RelayCommand]
    private Task GoToAuthorDetailAsync(Guid id)
    {
        navigationService.NavigateToAuthor(id);
        return Task.CompletedTask;
    }
    
    [RelayCommand]
    private Task GoToPlaylistDetailAsync(Guid id)
    {
        navigationService.NavigateToPlaylist(id);
        return Task.CompletedTask;
    }
    
    [RelayCommand]
    private Task GoToAlbumDetailAsync(Guid id)
    {
        navigationService.NavigateToAlbum(id);
        return Task.CompletedTask;
    }
}