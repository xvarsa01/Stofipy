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
    
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Playlists = (await facade.GetAllAsync(1, 10)).ToObservableCollection();
    }
    
    [RelayCommand]
    private Task GoToPlaylistDetailAsync(Guid id)
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