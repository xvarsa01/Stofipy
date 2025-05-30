using DotVVM.Framework.ViewModel;
using Stofipy.BL.Facades;
using Stofipy.BL.Facades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stofipy.App2.ViewModels
{
    public class MasterPageViewModel : DotvvmViewModelBase
    {
        private readonly IFilesInQueueFacade filesInQueueFacade;
        public SectionLeftViewModel SectionLeftVm { get; set; }
        public SectionRightViewModel SectionRightVm { get; set; }

        public MasterPageViewModel(IPlaylistFacade playlistFacade,
            IAuthorFacade authorFacade,
            IAlbumFacade albumFacade,
            IFilesInQueueFacade filesInQueueFacade)
        {
            this.filesInQueueFacade = filesInQueueFacade;
            SectionLeftVm = new SectionLeftViewModel(playlistFacade, authorFacade, albumFacade);
            SectionRightVm = new SectionRightViewModel(filesInQueueFacade);
        }

        public override async Task PreRender()
        {
            await base.PreRender();
        }

        public async Task PreviousSong()
        {
            await filesInQueueFacade.PreviousSong();
            await PreRender();
        }
        public async Task NextSong()
        {
            await filesInQueueFacade.NextSong();
            await PreRender();
        }
    }
}
