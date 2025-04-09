using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;

namespace Stofipy.DAL.Repositories;

public class FileRepository(StofipyDbContext dbContext) : RepositoryBase<FileEntity>(dbContext)
{
    private readonly DbSet<FileEntity> _dbSet = dbContext.Set<FileEntity>();

    public override Task<List<FileEntity>> GetAllAsync()
    {
        throw new NotImplementedException("This method is unsupported. Use the overload with paging.");
    }

    public override Task<List<FileEntity>> GetAllAsync(int pageNumber, int pageSize)
    {
        return _dbSet
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(e => e.Author)
            .ToListAsync();
    }

    public override Task<FileEntity?> GetByIdAsync(Guid id)
    {
        return _dbSet   
            .Include(e => e.Author)
            .SingleOrDefaultAsync(e => e.Id == id);
    }

    public async Task<List<FileEntity>> SearchInFilesAsync(string searchTerm)
    {
        return await _dbSet
            .Where(e => e.FileName.ToLower().Contains(searchTerm.ToLower()))
            .Include(e => e.Author)
            .ToListAsync();
    }
}