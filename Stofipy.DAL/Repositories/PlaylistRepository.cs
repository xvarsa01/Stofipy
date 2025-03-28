using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;

namespace Stofipy.DAL.Repositories;

public class PlaylistRepository(StofipyDbContext dbContext) : RepositoryBase<PlaylistEntity>(dbContext)
{
    private readonly DbSet<PlaylistEntity> _dbSet = dbContext.Set<PlaylistEntity>();

    public override async Task<PlaylistEntity?> GetByIdAsync(Guid id)
    {
        // includes are necessary for total length 
        return await _dbSet
            .Include(e => e.FilesInPlaylists)
            .ThenInclude(e => e.File)
            .SingleOrDefaultAsync(entity => entity.Id == id);
    }
    
    public async Task<List<PlaylistEntity>> SearchInPlaylistsAsync(string searchTerm)
    {
        return await _dbSet
            .Where(e => e.PlaylistName.ToLower().Contains(searchTerm.ToLower()))
            .ToListAsync();
    }
}