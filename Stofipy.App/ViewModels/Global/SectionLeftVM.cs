using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.Input;
using Stofipy.App.Services;
using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Models;

namespace Stofipy.App.ViewModels;

public partial class SectionLeftVM(IPlaylistFacade playlistFacade, IAuthorFacade authorFacade, INavigationService navigationService) : ViewModelBase
{
    public ObservableCollection<PlaylistListModel> Playlists { get; set; } = new()
    {
        new PlaylistListModel
        {
            Id = Guid.NewGuid(), PlaylistName = "daylist", Picture = "https://img.icons8.com/?size=100&id=14089&format=png&color=000000", Authors = []
        },
        new PlaylistListModel
        {
            Id = Guid.NewGuid(), PlaylistName = "TenerifeVaccay", Picture = "https://img.icons8.com/?size=100&id=14089&format=png&color=000000", Authors = []
        },
        new PlaylistListModel
        {
            Id = Guid.NewGuid(), PlaylistName = "4 veze", Picture = "https://img.icons8.com/?size=100&id=14089&format=png&color=000000", Authors = []
        },
    };

    public ObservableCollection<AuthorListModel> Authors { get; set; } = new();
    
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Authors = (await authorFacade.GetAllAsync(1, 10)).ToObservableCollection();
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
}