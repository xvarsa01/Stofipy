using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Mappers;
using Stofipy.BL.Models;
using Stofipy.DAL.Entities;
using Stofipy.DAL.Repositories;

namespace Stofipy.BL.Facades;

public class FilesInQueueFacade : FacadeBase<FilesInQueueRepository, FilesInQueueEntity, FilesInQueueModel, FilesInQueueModel>, IFilesInQueueFacade
{
    private readonly FilesInQueueRepository _repository;
    private readonly FilesInQueueModelMapper _modelMapper;
    public FilesInQueueFacade(FilesInQueueRepository repository, FilesInQueueModelMapper modelMapper) : base(repository, modelMapper)
    {
        _modelMapper = modelMapper;
        _repository = repository;
    }
}