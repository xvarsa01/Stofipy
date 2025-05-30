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
    public class SectionRightViewModel(
        IFilesInQueueFacade filesInQueueFacade) : DotvvmViewModelBase
    {
        public FilesInQueueModel NowPlaying { get; set; } = FilesInQueueModel.Empty;
        public List<FilesInQueueModel> PriorityQueue { get; set; } = [];
        public List<FilesInQueueModel> BasicQueue { get; set; } = [];
        public List<FilesInQueueModel> RecentlyPlayed { get; set; } = [];

        public bool DisplayRecentlyPlayed { get; set; } = false;

        public override async Task PreRender()
        {
            NowPlaying = await filesInQueueFacade.GetCurrentAsync();
            PriorityQueue = await filesInQueueFacade.GetAllPriorityFilesInQueueAsync();
            BasicQueue = await filesInQueueFacade.GetAllNonPriorityFilesInQueueAsync();
            RecentlyPlayed = await filesInQueueFacade.GetRecentFilesInQueueAsync(20);

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

        private async Task NextSong()
        {
            await filesInQueueFacade.NextSong();
            await PreRender();
        }

        private async Task PreviousSong()
        {
            await filesInQueueFacade.PreviousSong();
            await PreRender();
        }
    }
}

