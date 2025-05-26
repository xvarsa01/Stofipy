using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

//using Stofipy.App.Services.Interfaces;
using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Models;

namespace Stofipy.AppDotVVM.ViewModels;

public partial class AuthorDetailVM(
    IAuthorFacade facade,
    IFileFacade fileFacade) : MasterPageViewModel()
{
    private Guid Id { get; set; } = Guid.Parse("63E304C0-F051-436D-A466-3048C1C0D31F");
    public AuthorDetailModel Author { get; set; }

    private FileListModel SelectedFile { get; set; }
    public List<FileListModel> PopularFilesCurrentlyShowed { get; set; } = new();
    private List<FileListModel> PopularFiles5 { get; set; } = new();
    private List<FileListModel> PopularFiles10 { get; set; } = new();


    private bool _morePopularFilesShowed;
    private bool _following;

    public override async Task PreRender()
    {
        if (Author == null)
        {
            await LoadByIdAsync(Id);
        }

        await base.PreRender();
    }

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
        PopularFilesCurrentlyShowed = PopularFiles5;
    }

    [RelayCommand]
    private async Task ShowMorePopularFiles()
    {
        if (!_morePopularFilesShowed)
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
                PopularFilesCurrentlyShowed = PopularFiles10;
            }

            //SeeMoreText = "Show less";
            _morePopularFilesShowed = true;
        }
        else
        {
            PopularFiles10 = PopularFilesCurrentlyShowed;
            PopularFilesCurrentlyShowed = PopularFiles5;

            //SeeMoreText = "Show more";
            _morePopularFilesShowed = false;
        }
    }

    //[RelayCommand]
    //private Task SelectRowAsync(FileListModel item)
    //{
    //    if (SelectedFile != null)
    //        SelectedFile.IsSelected = false;

    //    SelectedFile = item;
    //    item.IsSelected = true;
    //    return Task.CompletedTask;
    //}


    public void PlayArtist()
    {
        Console.WriteLine($"Playing artist");
    }

    //[RelayCommand]
    //public void ToggleFollowCommand(FileListModel item)
    //{
    //    if (_following)
    //    {
    //        _following = false;
    //        //FollowText = "Follow";
    //    }
    //    else
    //    {
    //        _following = true;
    //        //FollowText = "Following";
    //    }
    //}
}