using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;

namespace Stofipy.DAL.Repositories;

public class FilesInQueueRepository (StofipyDbContext dbContext): RepositoryBase<FilesInQueueEntity>(dbContext)
{
    private readonly DbSet<FilesInQueueEntity> _dbSet = dbContext.Set<FilesInQueueEntity>();

    public override async Task<List<FilesInQueueEntity>> GetAllAsync()
    {
        return await _dbSet
            .Include(e => e.File)
            .ThenInclude(e => e.Author)
            .OrderBy(e => e.Index)
            .ToListAsync();
    }

    public int GetMaxIndex()
    {
        return _dbSet
            .Select(x => (int?)x.Index)
            .Max() ?? 0;
    }
}