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
    ICurrentStateService currentState,
    IMessengerService messengerService) : ViewModelBase(messengerService)
{
    public ObservableCollection<PlaylistListModel> Playlists { get; set; } = [];
    public FileType SelectedFileType { get; set; } = FileType.All;

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
    private async Task PlayItemAsync(Guid id)
    {
        await currentState.PlayPlaylist(id);
        // await currentState.PlayAuthor(id);
        // await currentState.PlayAlbum(id);
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