using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Mappers;
using Stofipy.BL.Models;
using Stofipy.DAL.Entities;
using Stofipy.DAL.Repositories;

namespace Stofipy.BL.Facades;

public class AlbumFacade : FacadeBase<AlbumRepository, AlbumEntity, AlbumListModel, AlbumDetailModel>, IAlbumFacade
{
    private readonly AlbumRepository _repository;
    private readonly AlbumModelMapper _modelMapper;
    public AlbumFacade(AlbumRepository repository, AlbumModelMapper modelMapper) : base(repository, modelMapper)
    {
        _modelMapper = modelMapper;
        _repository = repository;
    }
}