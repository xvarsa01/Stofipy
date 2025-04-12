using Stofipy.BL.Facades;
using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Models;

namespace Stofipy.App.ViewModels;

internal class ListOfPlaylistsViewModel()
{
    public List<PlaylistListModel> Playlists { get; set; } = new()
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
    
    // protected async Task LoadDataAsync()
    // {
    //     Playlists = await playlistFacade.GetAllAsync(1, 10);
    // }
}