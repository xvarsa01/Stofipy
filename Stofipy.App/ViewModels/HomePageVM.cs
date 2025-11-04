using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.Input;
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
    public bool MusicSelected { get; set; }
    public bool PodcastsSelected { get; set; }
    public bool AllSelected { get; set; } = true;
    
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Playlists = (await facade.GetAllAsync(1, 10)).ToObservableCollection();
    }

    [RelayCommand]
    private void SelectAll()
    {
        MusicSelected = false;
        PodcastsSelected = false;
        AllSelected = true;
    }
    
    [RelayCommand]
    private void SelectMusic()
    {
        MusicSelected = true;
        PodcastsSelected = false;
        AllSelected = false;
    }
    
    [RelayCommand]
    private void SelectPodcasts()
    {
        MusicSelected = false;
        PodcastsSelected = true;
        AllSelected = false;
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