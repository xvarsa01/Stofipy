using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;

namespace Stofipy.DAL.Repositories;

public class AlbumRepository(StofipyDbContext dbContext) : RepositoryBase<AlbumEntity>(dbContext)
{
    private readonly DbSet<AlbumEntity> _dbSet = dbContext.Set<AlbumEntity>();
    
    public override Task<List<AlbumEntity>> GetAllAsync()
    {
        throw new NotImplementedException("This method is unsupported. Use the overload with paging.");
    }
    public override Task<List<AlbumEntity>> GetAllAsync(int pageNumber, int pageSize)
    {
        return _dbSet
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(e => e.Author)
            .ToListAsync();
    }

    public override Task<AlbumEntity?> GetByIdAsync(Guid id)
    {
        return _dbSet   
            .Include(e => e.Author)
            .Include(e => e.FilesInAlbums)
            .ThenInclude(e => e.File)
            .SingleOrDefaultAsync(e => e.Id == id);
    }
    public async Task<List<AlbumEntity>> SearchInAlbumsAsync(string searchTerm)
    {
        return await _dbSet
            .Where(e => e.AlbumName.ToLower().Contains(searchTerm.ToLower()))
            .ToListAsync();
    }
}