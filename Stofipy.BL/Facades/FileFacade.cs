using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Mappers;
using Stofipy.BL.Models;
using Stofipy.DAL.Entities;
using Stofipy.DAL.Repositories;

namespace Stofipy.BL.Facades;

public class FileFacade : FacadeBase<FileRepository, FileEntity, FileListModel, FileDetailModel>, IFileFacade
{
    private readonly FileRepository _repository;
    private readonly FileModelMapper _modelMapper;
    public FileFacade(FileRepository repository, FileModelMapper modelMapper) : base(repository, modelMapper)
    {
        _modelMapper = modelMapper;
        _repository = repository;
    }
    
    public async Task<List<FileListModel>> GetMostPopularFiles(Guid authorId, int pageNumber, int pageSize )
    {
        List<FileEntity> entities = await _repository.GetMostPopularByAuthorAsync(authorId, pageNumber, pageSize);
        
        var models =  _modelMapper.MapToListModel(entities);
        return models;
    }
}