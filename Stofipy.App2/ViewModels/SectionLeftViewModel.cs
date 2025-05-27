using Stofipy.BL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stofipy.BL.Facades.Interfaces;

namespace Stofipy.App2.ViewModels
{
    public class SectionLeftViewModel(
        IPlaylistFacade playlistFacade,
        IAuthorFacade authorFacade,
        IAlbumFacade albumFacade) : MasterPageViewModel
    {
        public List<PlaylistListModel> Playlists { get; set; } = [];
        public List<AuthorListModel> Authors { get; set; } = [];
        public List<AlbumListModel> Albums { get; set; } = [];


        public override async Task PreRender()
        {
            await LoadDataAsync();
            await base.PreRender();
        }

        protected async Task LoadDataAsync()
        {
            Playlists = await playlistFacade.GetAllAsync(1, 10);
            Authors = await authorFacade.GetAllAsync(1, 10);
            Albums = await albumFacade.GetAllAsync(1, 10);
        }

        public void GoToAuthorDetailAsync(Guid id)
        {
            Context.RedirectToRoute("AuthorDetail", new { Id = id });
        }

        public void GoToPlaylistDetailAsync(Guid id)
        {
            Context.RedirectToRoute("PlaylistDetail", new { Id = id });
        }

        public void GoToAlbumDetailAsync(Guid id)
        {
            Context.RedirectToRoute("AlbumDetail", new { Id = id });
        }

        public void Test()
        {

        }
    }
}

