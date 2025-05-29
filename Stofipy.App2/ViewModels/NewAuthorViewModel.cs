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
    public class NewAuthorViewModel(
        IAuthorFacade authorFacade,
        IPlaylistFacade playlistFacade,
        IAlbumFacade albumFacade,
        IFilesInQueueFacade filesInQueueFacade) : MasterPageViewModel(playlistFacade, authorFacade, albumFacade, filesInQueueFacade)
    {

        public AuthorDetailModel Author { get; set; } = AuthorDetailModel.Empty;

        public void Save()
        {
            if (Author == null || Author.AuthorName == string.Empty)
            {
                throw new ArgumentException("Author name cannot be empty.");
            }
            authorFacade.CreateAsync(Author);
            Context.RedirectToRoute("Home");

        }
    }
}

