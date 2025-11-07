using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.Input;
using Stofipy.App.Enums;
using Stofipy.App.Services;
using Stofipy.App.Services.Interfaces;
using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Models;

namespace Stofipy.App.ViewModels;

public partial class HomePageVM(
    IPlaylistFacade facade,
    INavigationService navigationService,
    IMessengerService messengerService) : ViewModelBase(messengerService)
{
    public ObservableCollection<PlaylistListModel> Playlists { get; set; } = new();
    public FileType SelectedFileType { get; set; } = FileType.All;
    public bool MusicSelected => SelectedFileType == FileType.Music;
    public bool PodcastsSelected => SelectedFileType == FileType.Podcasts;
    public bool AllSelected => SelectedFileType == FileType.All;
    
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Playlists = (await facade.GetAllAsync(1, 10)).ToObservableCollection();
    }

    [RelayCommand]
    private void SelectFileType(FileType fileType)
    {
        SelectedFileType = fileType;
    }
    
    [RelayCommand]
    private Task PlayItemAsync(Guid id)
    {
        return Task.CompletedTask;
    }
    
    [RelayCommand]
    private Task OpenItemDetailAsync(Guid id)
    {
        navigationService.NavigateToPlaylist(id);
        return Task.CompletedTask;
    }
   
    [RelayCommand]
    private Task GoToYourShowsAsync()
    {
        navigationService.NavigateToYourShows();
        return Task.CompletedTask;
    }
    
    [RelayCommand]
    private Task GoToMadeForYouAsync()
    {
        navigationService.NavigateToMadeForYou();
        return Task.CompletedTask;
    }
    
    [RelayCommand]
    private Task GoToRecentlyPlayedAsync()
    {
        navigationService.NavigateToRecentlyPlayed();
        return Task.CompletedTask;
    }
    
    [RelayCommand]
    private Task GoToPopularRadioAsync()
    {
        navigationService.NavigateToPopularRadio();
        return Task.CompletedTask;
    }
}