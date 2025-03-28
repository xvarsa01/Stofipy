using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;

namespace Stofipy.DAL.Repositories;

public class AlbumRepository(StofipyDbContext dbContext) : RepositoryBase<AlbumEntity>(dbContext)
{
    private readonly DbSet<AlbumEntity> _dbSet = dbContext.Set<AlbumEntity>();
    
    public async Task<List<AlbumEntity>> SearchInalbumsAsync(string searchTerm)
    {
        return await _dbSet
            .Where(e => e.AlbumName.ToLower().Contains(searchTerm.ToLower()))
            .ToListAsync();
    }
}