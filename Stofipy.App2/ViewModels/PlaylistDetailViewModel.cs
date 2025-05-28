using CommunityToolkit.Mvvm.Input;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel;
using Stofipy.BL.Facades;
using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stofipy.App2.ViewModels
{
    public class PlaylistDetailViewModel(
        IPlaylistFacade playlistFacade,
        IAuthorFacade authorFacade,
        IAlbumFacade albumFacade,
        IFilesInPlaylistFacade filesInPlaylistFacade,
        IFilesInQueueFacade filesInQueueFacade) : MasterPageViewModel(playlistFacade, authorFacade, albumFacade, filesInQueueFacade)
    {
        [FromRoute("id")]
        private Guid Id { get; set; }
        public PlaylistDetailModel Playlist { get; set; } = null!;
        public List<FilesInPlaylistModel> Files { get; set; }

        public override async Task PreRender()
        {
            if (Playlist == null)
            {
                await LoadDataAsync();
            }
            await base.PreRender();
        }

        private async Task LoadDataAsync()
        {
            Playlist = await playlistFacade.GetByIdAsync(Id) ?? PlaylistDetailModel.Empty;
            Files = await filesInPlaylistFacade.GetAllAsync(Id, 1, 10);
        }


        public async Task PlayPlaylistAsync()
        {
            await filesInQueueFacade.AddPlaylistToQueue(Playlist.Id, false);
        }

        public void Shuffle()
        {

        }

        public void Download()
        {

        }

    }
}

