using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.Input;
using Stofipy.App.Services;
using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Models;

namespace Stofipy.App.ViewModels;

public partial class ListOfPlaylistsVM(IPlaylistFacade facade, INavigationService navigationService) : ViewModelBase
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
}