using Stofipy.BL.Models;
using Stofipy.DAL.Entities;

namespace Stofipy.BL.Facades.Interfaces;

public interface IFilesInPlaylistFacade : IFacade<FilesInPlaylistEntity, FilesInPlaylistModel, FilesInPlaylistModel>
{
    Task<List<FilesInPlaylistModel>> GetAllAsync(Guid playlistId, int pageNumber, int pageSize);
    Task<List<FilesInPlaylistModel>> SearchInPlaylistAsync(Guid playlistId, string searchTerm);
    Task SortByDurationAsync(Guid playlistId);
    Task SortByTitleAsync(Guid playlistId);
    Task SortByArtistAsync(Guid playlistId);
    Task SortByAlbumAsync(Guid playlistId);
    Task SortByCustomOrderAsync(Guid playlistId);
    
}