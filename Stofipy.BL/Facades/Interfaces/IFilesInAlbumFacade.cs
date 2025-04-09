using Stofipy.BL.Models;
using Stofipy.DAL.Entities;

namespace Stofipy.BL.Facades.Interfaces;

public interface IFilesInAlbumFacade : IFacade<FilesInAlbumEntity, FilesInAlbumModel, FilesInAlbumModel>
{
    Task<List<FilesInAlbumModel>> GetAllByAlbumIdAsync(Guid albumId);
    Task<Guid> CreateAsync(FilesInAlbumModel model, Guid albumId);
}