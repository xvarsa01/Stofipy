using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Hosting;
using Stofipy.BL.Facades.Interfaces;

namespace Stofipy.App2.ViewModels
{
    public class HomeViewModel : MasterPageViewModel
    {
        public HomeViewModel(Stofipy.BL.Facades.Interfaces.IPlaylistFacade playlistFacade, Stofipy.BL.Facades.Interfaces.IAuthorFacade authorFacade, Stofipy.BL.Facades.Interfaces.IAlbumFacade albumFacade, Stofipy.BL.Facades.Interfaces.IFilesInQueueFacade filesInQueueFacade)
            : base(playlistFacade, authorFacade, albumFacade, filesInQueueFacade)
        {
        }
    }
}

