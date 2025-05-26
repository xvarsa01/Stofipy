using CommunityToolkit.Mvvm.Input;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel;
using Stofipy.BL.Facades;
using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stofipy.AppDotVVM.ViewModels
{
    public class LeftSectionVM(
    IPlaylistFacade playlistFacade,
    IAuthorFacade authorFacade) : MasterPageViewModel
    {
        //private readonly INavigationService _navigationService = navigationService;
        public List<PlaylistListModel> Playlists { get; set; } = [];
        public List<AuthorListModel> Authors { get; set; } = [];


        public override async Task PreRender()
        {
            await LoadDataAsync();
            await base.PreRender();
        }

        protected override async Task LoadDataAsync()
        {
            await base.LoadDataAsync();
            Playlists = (await playlistFacade.GetAllAsync(1, 10));
            Authors = (await authorFacade.GetAllAsync(1, 10));
        }

        public void GoToAuthorDetailAsync(Guid id)
        {
            //_navigationService.NavigateTo("AutorDetailPage", new { id });
            //navigationService.NavigateToAuthor(id);
            //return Task.CompletedTask;
        }

        public void GoToPlaylistDetailAsync(Guid id)
        {
            //navigationService.NavigateToPlaylist(id);
            //return Task.CompletedTask;
        }
    }
}

