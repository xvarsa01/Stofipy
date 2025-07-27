using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;

namespace Stofipy.DAL.Repositories;

public class RepositoryBase<TEntity>(StofipyDbContext dbContext) : IRepository<TEntity> 
    where TEntity : class, IEntity
{
    private readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();

    public IQueryable<TEntity> Query()
    {
        return _dbSet.AsQueryable();
    }
    
    public virtual async Task<List<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<List<TEntity>> GetAllAsync(int pageNumber, int pageSize)
    {
        return await _dbSet
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();
    }
    public virtual async Task<TEntity?> GetByIdAsync(Guid id)
    {
            return await _dbSet.SingleOrDefaultAsync(entity => entity.Id == id);
    }

    public async Task DeleteAsync(Guid entityId)
    {
        _dbSet.Remove(await _dbSet.SingleAsync(i => i.Id == entityId).ConfigureAwait(false));
        await dbContext.SaveChangesAsync();
    }

    public async Task<Guid> InsertAsync(TEntity entity)
    {
        var createdEntity = (await _dbSet.AddAsync(entity)).Entity;
        await dbContext.SaveChangesAsync(); 
        return createdEntity.Id;
    }

    public async Task<Guid?> UpdateAsync(TEntity entity)
    {
        if (await ExistsAsync(entity))
        {
            _dbSet.Attach(entity);
            var updatedEntity = dbContext.Set<TEntity>().Update(entity).Entity;
            await dbContext.SaveChangesAsync();

            return updatedEntity.Id;
        }
        else
        {
            return null;
        }
    }
    
    public async ValueTask<bool> ExistsAsync(TEntity entity)
    {
        return entity.Id != Guid.Empty && await _dbSet.AnyAsync(e => e.Id == entity.Id);
    }
}