using DotVVM.Framework.ViewModel;
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
        public SectionLeftViewModel SectionLeftVm { get; set; }
        public SectionRightViewModel SectionRightVm { get; set; }

        public MasterPageViewModel(IPlaylistFacade playlistFacade,
            IAuthorFacade authorFacade,
            IAlbumFacade albumFacade,
            IFilesInQueueFacade filesInQueueFacade)
        {
            SectionLeftVm = new SectionLeftViewModel(playlistFacade, authorFacade, albumFacade);
            SectionRightVm = new SectionRightViewModel(filesInQueueFacade);
        }

        public override async Task PreRender()
        {
            await base.PreRender();
        }

    }
}
