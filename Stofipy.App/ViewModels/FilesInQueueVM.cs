using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.Input;
using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Models;

namespace Stofipy.App.ViewModels;

public partial class FilesInQueueVM(IFilesInQueueFacade facade) : ViewModelBase
{
    public ObservableCollection<FilesInQueueModel> Queue { get; set; } = null!;

    public FilesInQueueModel NowPlaying { get; set; } = new()
    {
        Id = Guid.NewGuid(),
        FileName = "Fake ID",
        AuthorName = "Rithon",
        Picture = "https://img.icons8.com/?size=100&id=14089&format=png&color=000000",
        Index = 1,
        FileId = Guid.NewGuid(),
        PriorityQueue = true
    };

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Queue = (await facade.GetAllAsync()).ToObservableCollection();
    }


    [RelayCommand]
    private void ShowQueue()
    {
        
    }

    [RelayCommand]
    private void ShowRecentlyPlayed()
    {
        
    }
    
    [RelayCommand]
    private void CloseRightSection()
    {
        
    }
}