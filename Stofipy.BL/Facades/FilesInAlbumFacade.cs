using Microsoft.EntityFrameworkCore;
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
    
    public async Task<List<FilesInAlbumModel>> GetAllByAlbumIdAsync(Guid albumId)
    {
        List<FilesInAlbumEntity> entities = await Repository.GetAllByAlbumIdAsync(albumId);
        
        var models =  _modelMapper.MapToListModel(entities);
        return models;
    }

    public override Task<Guid> CreateAsync(FilesInAlbumModel model)
    {
        throw new NotImplementedException("Use overload with Guid albumId instead");
    }

    public async Task<Guid> CreateAsync(FilesInAlbumModel model, Guid albumId)
    {
        GuardCollectionsAreNotSet(model);
        int maxIndex = await _repository
            .Query() // Expose IQueryable from your repository
            .Where(f => f.AlbumId == albumId)
            .Select(f => (int?)f.Index)
            .MaxAsync() ?? 0;
        
        FilesInAlbumEntity entityFromModel = _modelMapper.MapToEntity(model, albumId);
        entityFromModel.Index = maxIndex + 1;
        entityFromModel.AlbumId = albumId;
        
        Guid createdEntityId = await Repository.InsertAsync(entityFromModel);
        return createdEntityId;
    }
    
    
    public override async Task DeleteAsync(Guid id)
    {
        try
        {
            FilesInAlbumEntity? entity = await Repository.GetByIdAsync(id);
            if (entity == null)
                throw new InvalidOperationException("Entity not found.");
            
            var albumId = entity.AlbumId;
            var deletedIndex = entity.Index;
            
            await Repository.DeleteAsync(id);
            
            var filesInAlbumEntity = await _repository.GetAllByAlbumIdAsync(albumId);
            var itemsToUpdate = filesInAlbumEntity
                .Where(f => f.Index > deletedIndex)
                .ToList();
            
            foreach (var item in itemsToUpdate)
            {
                item.Index--;
                await _repository.UpdateAsync(item);
            }
        }
        catch (DbUpdateException e)
        {
            throw new InvalidOperationException("Entity deletion failed.", e);
        }
    }
}