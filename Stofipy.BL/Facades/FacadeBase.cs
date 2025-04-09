using System.Collections;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Mappers.InterfaceBase;
using Stofipy.DAL.Entities;
using Stofipy.DAL.Repositories;

namespace Stofipy.BL.Facades;

public class FacadeBase<TRepository, TEntity, TListModel, TDetailModel> : IFacade<TEntity, TListModel, TDetailModel>
    where TRepository : IRepository<TEntity>
    where TEntity : class, IEntity
    where TListModel : class
    where TDetailModel : class
{
    
    protected readonly IModelMapper<TEntity, TListModel, TDetailModel> ModelMapper;
    protected readonly TRepository Repository;

    public FacadeBase(TRepository repository, IModelMapper<TEntity, TListModel, TDetailModel> modelMapper)
    {
        ModelMapper = modelMapper;
        Repository = repository;
    }

    public virtual async Task<List<TListModel>> GetAllAsync()
    {
        List<TEntity> entities = await Repository.GetAllAsync();
        
        var models =  ModelMapper.MapToListModel(entities);
        return models;
    }
    
    public virtual async Task<List<TListModel>> GetAllAsync(int pageNumber, int pageSize)
    {
        List<TEntity> entities = await Repository.GetAllAsync(pageNumber, pageSize);
        
        var models =  ModelMapper.MapToListModel(entities);
        return models;
    }
    
    public virtual async Task<TDetailModel?> GetByIdAsync(Guid id)
    {
        TEntity? entity = await Repository.GetByIdAsync(id);

        return entity is null
            ? null
            : ModelMapper.MapToDetailModel(entity);
    }
    
    public virtual async Task<Guid?> UpdateAsync(TDetailModel model)
    {
        GuardCollectionsAreNotSet(model);
        TEntity entity = ModelMapper.MapToEntity(model);
        
        Guid? updatedEntityId = await Repository.UpdateAsync(entity);
        return updatedEntityId;
    }

    public virtual async Task<Guid> CreateAsync(TDetailModel model)
    {
        GuardCollectionsAreNotSet(model);
        TEntity entity = ModelMapper.MapToEntity(model);
        
        Guid createdEntityId = await Repository.InsertAsync(entity);
        return createdEntityId;
    }
    
    public virtual async Task DeleteAsync(Guid id)
    {
        try
        {
            await Repository.DeleteAsync(id);
        }
        catch (DbUpdateException e)
        {
            throw new InvalidOperationException("Entity deletion failed.", e);
        }
    }
    
    public static void GuardCollectionsAreNotSet(TDetailModel model)
    {
        IEnumerable<PropertyInfo> collectionProperties = model
            .GetType()
            .GetProperties()
            .Where(i => typeof(ICollection).IsAssignableFrom(i.PropertyType));

        if (collectionProperties.Any(collectionProperty => collectionProperty.GetValue(model) is ICollection { Count: > 0 }))
        {
            throw new InvalidOperationException(
                "Current BL and DAL infrastructure disallows insert or update of models with adjacent collections.");
        }
    }
}