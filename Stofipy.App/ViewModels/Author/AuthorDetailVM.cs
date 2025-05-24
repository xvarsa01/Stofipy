using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Models;

namespace Stofipy.App.ViewModels.Author;

public partial class AuthorDetailVM(IAuthorFacade facade, IFileFacade fileFacade) : ViewModelBase
{
    private Guid Id { get; set; }
    public AuthorDetailModel? Author { get; set; }
    
    private FileListModel? SelectedFile {get; set; }
    public ObservableCollection<FileListModel> PopularFilesCurrentlyShowed { get; set; } = new();
    private List<FileListModel> PopularFiles5 { get; set; } = new();
    private List<FileListModel> PopularFiles10 { get; set; } = new();

    [ObservableProperty]
    private string _seeMoreText = "See more";
    [ObservableProperty]
    private string _followText = "Follow";

    
    private bool _morePopularFilesShowed;
    private bool _following;

    public async Task LoadByIdAsync(Guid id)
    {
        Id = id;
        await LoadDataAsync();
    }
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Author = await facade.GetByIdAsync(Id);
        PopularFiles5 = await fileFacade.GetMostPopularFiles(Id, 1, 5);
        PopularFilesCurrentlyShowed = PopularFiles5.ToObservableCollection();
    }
    
    [RelayCommand]
    private async Task ShowMorePopularFiles()
    {
        if (! _morePopularFilesShowed)
        {
            if (PopularFiles10.Count == 0)
            {
                var newFiles = await fileFacade.GetMostPopularFiles(Id, 2, 5);
                foreach (var file in newFiles)
                {
                    PopularFilesCurrentlyShowed.Add(file);
                }
            }
            else
            {
                PopularFilesCurrentlyShowed = PopularFiles10.ToObservableCollection();
            }
            
            SeeMoreText = "Show less";
            _morePopularFilesShowed = true;
        }
        else
        {
            PopularFiles10 = PopularFilesCurrentlyShowed.ToList();
            PopularFilesCurrentlyShowed = PopularFiles5.ToObservableCollection();
            
            SeeMoreText = "Show more";
            _morePopularFilesShowed = false;
        }
    }
    
    [RelayCommand]
    private Task SelectRowAsync(FileListModel item)
    {
        if (SelectedFile != null)
            SelectedFile.IsSelected = false;
    
        SelectedFile = item;
        item.IsSelected = true;
        return Task.CompletedTask;
    }

    [RelayCommand]
    private Task PlayArtis(FileListModel item)
    {
        return Task.CompletedTask;
    }
    
    [RelayCommand]
    private Task ToggleFollow(FileListModel item)
    {
        if (_following)
        {
            _following = false;
            FollowText = "Follow";
        }
        else
        {
            _following = true;
            FollowText = "Following";
        }
        return Task.CompletedTask;
    }
}