using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;
using Stofipy.DAL.Utilities;

namespace Stofipy.DAL.Repositories;

public class PlaylistRepository(StofipyDbContext dbContext) : RepositoryBase<PlaylistEntity>(dbContext)
{
    private readonly DbSet<PlaylistEntity> _dbSet = dbContext.Set<PlaylistEntity>();

    public override async Task<PlaylistEntity?> GetByIdAsync(Guid id)
    {
        // includes are necessary for total length 
        return await _dbSet
            .Include(e => e.CreatedBy)
            .Include(e => e.FilesInPlaylists)
            .ThenInclude(e => e.File)
            .SingleOrDefaultAsync(entity => entity.Id == id);
    }
    
    public async Task<(List<PlaylistEntity> SimilarPlaylists, PlaylistEntity? BestPlaylist, int BestDifference)> SearchInPlaylistsAsync(string searchTerm)
    {
        var lowerSearchTerm = searchTerm.ToLower();
        var prefix = searchTerm.Length >= 2 
            ? searchTerm[..2].ToLower() 
            : searchTerm.ToLower();
        
        var candidates = await _dbSet
            .Where(e => EF.Functions.Like(e.PlaylistName.ToLower(), $"{prefix}%")  // word at start
                        || EF.Functions.Like(e.PlaylistName.ToLower(), $"% {prefix}%"))  // word after space
            // .OrderByDescending(e => e.PlayCount)
            .Take(500)
            .ToListAsync();
        
        var ranked = candidates
            .Select(e => new
            {
                Entity = e,
                Distance = StringSimilarity<PlaylistEntity>.LevenshteinDistance(e.PlaylistName.ToLower(), lowerSearchTerm)
            })
            .OrderBy(x => x.Distance)
            .ToList();

        var similarPlaylists = ranked
            .Take(5)
            .Select(x => x.Entity)
            .ToList();

        var best = ranked.FirstOrDefault();
        return (similarPlaylists, best?.Entity, best?.Distance ?? int.MaxValue);
    }

}