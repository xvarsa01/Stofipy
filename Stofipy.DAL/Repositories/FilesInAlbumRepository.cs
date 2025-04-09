using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;

namespace Stofipy.DAL.Repositories;

public class FilesInAlbumRepository(StofipyDbContext dbContext) : RepositoryBase<FilesInAlbumEntity>(dbContext)
{
    private readonly DbSet<FilesInAlbumEntity> _dbSet = dbContext.Set<FilesInAlbumEntity>();

    public override Task<FilesInAlbumEntity?> GetByIdAsync(Guid id)
    {
        return _dbSet
            .Include(e => e.File)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public override Task<List<FilesInAlbumEntity>> GetAllAsync()
    {
        throw new NotImplementedException("This method is unsupported. Use the overload GetAllByAlbumIdAsync.");
    }
    
    public async Task<List<FilesInAlbumEntity>> GetAllByAlbumIdAsync(Guid albumId)
    {
        return await _dbSet
            .Where(fileInAlbum => fileInAlbum.AlbumId == albumId )
            .OrderBy(e => e.Index)
            .Include(e => e.File)
            .ToListAsync();
    }
    public async Task<List<FilesInAlbumEntity>> GetAllByAlbumIdAsync(Guid albumId, int pageNumber, int pageSize)
    {
        return await _dbSet
            .Where(fileInAlbum => fileInAlbum.AlbumId == albumId )
            .OrderBy(e => e.Index)
            .Include(e => e.File)
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();
    }
}