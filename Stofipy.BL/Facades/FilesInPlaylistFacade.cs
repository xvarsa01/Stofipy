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
}