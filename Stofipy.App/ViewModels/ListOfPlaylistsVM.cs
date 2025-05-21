using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.Input;
using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Models;

namespace Stofipy.App.ViewModels;

public partial class ListOfPlaylistsVM(IPlaylistFacade facade) : ViewModelBase
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
    
    // protected override async Task LoadDataAsync()
    // {
    //     await base.LoadDataAsync();
    //     Playlists = (await facade.GetAllAsync(1, 10)).ToObservableCollection();
    //     OnPropertyChanged(nameof(Playlists));
    // }
        
    [RelayCommand]
    private async Task GoToDetailAsync(Guid id)
    {
        // Playlists.First().PlaylistName += "test";
        
        // await navigationService.GoToAsync<>();
    }
}