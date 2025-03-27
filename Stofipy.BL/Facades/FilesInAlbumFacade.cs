using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Mappers;
using Stofipy.BL.Models;
using Stofipy.DAL.Entities;
using Stofipy.DAL.Repositories;

namespace Stofipy.BL.Facades;

public class FilesInAlbumFacade : FacadeBase<FilesInAlbumRepository, FilesInAlbumEntity, FilesInAlbumModel, FilesInAlbumModel>, IFilesInAlbumFacade
{
    private readonly FilesInAlbumRepository _repository;
    private readonly FilesInAlbumModelMapper _modelMapper;
    public FilesInAlbumFacade(FilesInAlbumRepository repository, FilesInAlbumModelMapper modelMapper) : base(repository, modelMapper)
    {
        _modelMapper = modelMapper;
        _repository = repository;
    }
}