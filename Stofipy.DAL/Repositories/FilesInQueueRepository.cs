using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;

namespace Stofipy.DAL.Repositories;

public class FilesInQueueRepository (StofipyDbContext dbContext): RepositoryBase<FilesInQueueEntity>(dbContext)
{
    private readonly DbSet<FilesInQueueEntity> _dbSet = dbContext.Set<FilesInQueueEntity>();

    public override async Task<List<FilesInQueueEntity>> GetAllAsync()
    {
        var query = _dbSet.AsQueryable();
        query = IncludeAuthorAndDefaultAlbums(query);
            
        return await query
            .OrderBy(e => e.PriorityQueue)
            .ThenBy(e => e.Index)
            .ToListAsync();
    }
    public async Task<List<FilesInQueueEntity>> GetAllPriorityAsync()
    {
        var query = _dbSet
            .Where(e => e.PriorityQueue == true)
            .Where(e => e.Index > 0);
        query = IncludeAuthorAndDefaultAlbums(query);
        
        return await query
            .OrderBy(e => e.Index)
            .ToListAsync();
    }
    
    public async Task<List<FilesInQueueEntity>> GetAllNonPriorityAsync()
    {
        var query = _dbSet
            .Where(e => e.PriorityQueue == false)
            .Where(e => e.Index > 0);;
        query = IncludeAuthorAndDefaultAlbums(query);
        
        return await query
            .OrderBy(e => e.Index)
            .ToListAsync();
    }

    public async Task<FilesInQueueEntity?> GetByIndexAsync(int index, bool priority)
    {
        if (priority)
        {
            return await _dbSet
                .Where(e => e.PriorityQueue == true)
                .SingleOrDefaultAsync(e => e.Index == index);
        }
        else
        {
            return await _dbSet
                .Where(e => e.PriorityQueue == false)
                .SingleOrDefaultAsync(e => e.Index == index);
        }
    }

    public int GetMaxPriorityIndex()
    {
        return _dbSet
            .Where(e => e.PriorityQueue == true)
            .Select(x => (int?)x.Index)
            .Max() ?? 0;
    }
    public int GetMaxNonPriorityIndex()
    {
        return _dbSet
            .Where(e => e.PriorityQueue == false)
            .Select(x => (int?)x.Index)
            .Max() ?? 0;
    }
    
    private IQueryable<FilesInQueueEntity> IncludeAuthorAndDefaultAlbums(IQueryable<FilesInQueueEntity> query)
    {
        return query
            .Include(e => e.File)
            .ThenInclude(e => e.Author)
            .Include(e => e.File)
            .ThenInclude(e => e.DefaultAlbum);
    }
}