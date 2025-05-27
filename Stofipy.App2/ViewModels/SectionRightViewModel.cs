using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Hosting;
using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Models;

namespace Stofipy.App2.ViewModels
{
    public class SectionRightViewModel(
        IFilesInQueueFacade facade) : MasterPageViewModel
    {
        public FilesInQueueModel NowPlaying { get; set; } = FilesInQueueModel.Empty;
        public List<FilesInQueueModel> PriorityQueue { get; set; } = [];
        public List<FilesInQueueModel> BasicQueue { get; set; } = [];
        public List<FilesInQueueModel> RecentlyPlayed { get; set; } = [];

        public bool DisplayRecentlyPlayed { get; set; } = false;

        public override async Task PreRender()
        {
            NowPlaying = await facade.GetCurrentAsync();
            PriorityQueue = await facade.GetAllPriorityFilesInQueueAsync();
            BasicQueue = await facade.GetAllNonPriorityFilesInQueueAsync();
            RecentlyPlayed = await facade.GetRecentFilesInQueueAsync(20);

            await base.PreRender();
        }

        public async Task ShowRecentlyPlayed()
        {
            DisplayRecentlyPlayed = true;
            await PreRender();
        }

        public async Task ShowQueue()
        {
            DisplayRecentlyPlayed = false;
            await PreRender();
        }

    }
}

