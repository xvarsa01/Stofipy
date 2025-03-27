using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Mappers;
using Stofipy.BL.Models;
using Stofipy.DAL.Entities;
using Stofipy.DAL.Repositories;

namespace Stofipy.BL.Facades;

public class PlaylistFacade : FacadeBase<PlaylistRepository, PlaylistEntity, PlaylistListModel, PlaylistDetailModel>, IPlaylistFacade
{
    private readonly PlaylistRepository _repository;
    private readonly PlaylistModelMapper _modelMapper;
    public PlaylistFacade(PlaylistRepository repository, PlaylistModelMapper modelMapper) : base(repository, modelMapper)
    {
        _modelMapper = modelMapper;
        _repository = repository;
    }
}