using DotVVM.Framework.ViewModel;
using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stofipy.App2.ViewModels
{
    public class AuthorDetailViewModel(
        IAuthorFacade authorFacade,
        IPlaylistFacade playlistFacade,
        IAlbumFacade albumFacade,
        IFilesInQueueFacade filesInQueueFacade,
        IFileFacade fileFacade) : MasterPageViewModel(playlistFacade, authorFacade, albumFacade, filesInQueueFacade)
    {
        private readonly IAuthorFacade _authorFacade = authorFacade;

        [FromRoute("id")]
        private Guid Id { get; set; }
        public AuthorDetailModel Author { get; set; }

        private FileListModel SelectedFile { get; set; }
        public List<FileListModel> PopularFilesCurrentlyShowed { get; set; } = new();
        public List<FileListModel> PopularFiles5 { get; set; } = new();
        public List<FileListModel> PopularFiles10 { get; set; } = new();


        public bool MorePopularFilesShowed { get; set; } = false;
        public string SeeMoreText { get; set; } = "Show more";
        private bool _following = false;


        public override async Task PreRender()
        {
            if (Author == null)
            {
                await LoadDataAsync();
            }
            
            await base.PreRender();
        }

        private async Task LoadDataAsync()
        {
            Author = await _authorFacade.GetByIdAsync(Id);
            PopularFiles5 = await fileFacade.GetMostPopularFiles(Id, 1, 5);
            PopularFilesCurrentlyShowed = PopularFiles5;
        }

        public async Task ShowMorePopularFiles()
        {
            if (!MorePopularFilesShowed)
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

                SeeMoreText = "Show less";
                MorePopularFilesShowed = true;
            }
            else
            {
                PopularFiles10 = PopularFilesCurrentlyShowed;
                PopularFilesCurrentlyShowed = PopularFiles5;

                SeeMoreText = "Show more";
                MorePopularFilesShowed = false;
            }
        }


        public void PlayArtist()
        {
            Console.WriteLine($"Playing artist");
        }

    }
}

