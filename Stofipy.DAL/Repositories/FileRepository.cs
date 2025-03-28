using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;

namespace Stofipy.DAL.Repositories;

public class FileRepository(StofipyDbContext dbContext) : RepositoryBase<FileEntity>(dbContext)
{
    private readonly DbSet<FileEntity> _dbSet = dbContext.Set<FileEntity>();
    
    public async Task<List<FileEntity>> SearchInFilesAsync(string searchTerm)
    {
        return await _dbSet
            .Where(e => e.FileName.ToLower().Contains(searchTerm.ToLower()))
            .Include(e => e.Author)
            .ToListAsync();
    }
}