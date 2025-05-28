using DotVVM.Framework.ViewModel;
using Stofipy.BL.Facades;
using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stofipy.App2.ViewModels
{
    public class AlbumDetailViewModel(
        IAuthorFacade authorFacade,
        IPlaylistFacade playlistFacade,
        IAlbumFacade albumFacade,
        IFilesInAlbumFacade filesInAlbumFacade,
        IFilesInQueueFacade filesInQueueFacade) : MasterPageViewModel(playlistFacade, authorFacade, albumFacade, filesInQueueFacade)
    {
        [FromRoute("id")]
        private Guid Id { get; set; }
        public AlbumDetailModel Album { get; set; } = null!;

        public List<FilesInAlbumModel> Files { get; set; } = [];


        public override async Task PreRender()
        {
            if (Album == null)
            {
                await LoadDataAsync();
            }

            await base.PreRender();
        }

        private async Task LoadDataAsync()
        {
            Album = await albumFacade.GetByIdAsync(Id) ?? AlbumDetailModel.Empty;
            Files = await filesInAlbumFacade.GetAllByAlbumIdAsync(Id);
        }

        public async Task PlayAlbumAsync()
        {
            await filesInQueueFacade.AddAlbumToQueue(Album.Id, false);
        }
        public void Shuffle()
        {

        }

        public void Download()
        {

        }
    }
}
