using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Stofipy.BL.Models;

namespace Stofipy.App.ViewModels;

public partial class PlaylistDetailVM : ViewModelBase
{
    public PlaylistDetailModel Playlist { get; set; } = new ()
    {
        Id = Guid.NewGuid(),
        PlaylistName = "Tenerife Vacay",
        Description = null,
        Picture = "https://img.icons8.com/?size=100&id=14089&format=png&color=000000",
        Length = 0,
        IsPublic = false,
    };
    
    private FilesInPlaylistModel? SelectedFile {get; set; }
    public ObservableCollection<FilesInPlaylistModel> Files { get; init; } = new()
    {
        new FilesInPlaylistModel
        {
            Id = Guid.Parse("1F794072-4CFE-4030-B03D-05DB93B3EC07"),
            IndexActual = 1,
            IndexCustom = 1,
            FileId = Guid.NewGuid(),
            FileName = "TEST FILE",
            AuthorName = "Author of the file",
            DefaultAlbumName = "Default album",
            Length = 120,
            Picture = "https://img.icons8.com/?size=100&id=14089&format=png&color=000000",
        },
        new FilesInPlaylistModel
        {
            Id = Guid.Parse("1F794072-4CFE-4030-B03D-05DB93B3E123"),
            IndexActual = 2,
            IndexCustom = 2,
            FileId = Guid.NewGuid(),
            FileName = "TEST FILE 2",
            AuthorName = "Author of the file2",
            DefaultAlbumName = "Default album",
            Length = 150,
            Picture = "https://img.icons8.com/?size=100&id=14089&format=png&color=000000",
        },
        new FilesInPlaylistModel
        {
            Id = Guid.Parse("1F794072-4CFE-4030-B03D-05DB93B3E123"),
            IndexActual = 3,
            IndexCustom = 3,
            FileId = Guid.NewGuid(),
            FileName = "TEST FILE 3",
            AuthorName = "Author of the file3",
            DefaultAlbumName = "Default album",
            Length = 180,
            Picture = "https://img.icons8.com/?size=100&id=14089&format=png&color=000000",
        }
    };
            
    [RelayCommand]
    private async Task SelectRowAsync(FilesInPlaylistModel item)
    {
        Files.First().DefaultAlbumName += "test";
        
        if (SelectedFile != null)
            SelectedFile.IsSelected = false;
    
        SelectedFile = item;
        item.IsSelected = true;
    }
}