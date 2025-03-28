using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Mappers;
using Stofipy.BL.Models;
using Stofipy.DAL.Entities;
using Stofipy.DAL.Repositories;

namespace Stofipy.BL.Facades;

public class FilesInPlaylistFacade : FacadeBase<FilesInPlaylistRepository, FilesInPlaylistEntity, FilesInPlaylistModel, FilesInPlaylistModel>, IFilesInPlaylistFacade
{
    private readonly FilesInPlaylistRepository _repository;
    private readonly FilesInPlaylistModelMapper _modelMapper;
    public FilesInPlaylistFacade(FilesInPlaylistRepository repository, FilesInPlaylistModelMapper modelMapper) : base(repository, modelMapper)
    {
        _modelMapper = modelMapper;
        _repository = repository;
    }

    public async Task<List<FilesInPlaylistModel>> GetAllAsync(int pageNumber, int pageSize, Guid playlistId)
    {
        List<FilesInPlaylistEntity> entities = await Repository.GetAllAsync(pageNumber, pageSize, playlistId);
        
        var models =  _modelMapper.MapToListModel(entities);
        return models;
    }
    public async Task SortByDurationAsync(Guid playlistId)
    {
        await _repository.SortByDurationAsync(playlistId);
    }

    public async Task SortByTitleAsync(Guid playlistId)
    {
        await _repository.SortByTitleAsync(playlistId);
    }

    public async Task SortByArtistAsync(Guid playlistId)
    {
        await _repository.SortByArtistAsync(playlistId);
    }

    public async Task SortByAlbumAsync(Guid playlistId)
    {
        await _repository.SortByAlbumAsync(playlistId);
    }

    public async Task SortByCustomOrderAsync(Guid playlistId)
    {
        await _repository.SortByCustomOrderAsync(playlistId);
    }
    
}