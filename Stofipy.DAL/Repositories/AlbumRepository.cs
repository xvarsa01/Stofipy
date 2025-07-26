using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;
using Stofipy.DAL.Utilities;

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
    
    public async Task<(List<AlbumEntity> SimilarAlbums, AlbumEntity? BestAlbum, int BestDifference)> SearchInAlbumsAsync(string searchTerm)
    {
        var lowerSearchTerm = searchTerm.ToLower();
        var prefix = searchTerm.Length >= 2 
            ? searchTerm[..2].ToLower() 
            : searchTerm.ToLower();
        
        var candidates = await _dbSet
            .Where(e => EF.Functions.Like(e.AlbumName.ToLower(), $"{prefix}%")  // word at start
                        || EF.Functions.Like(e.AlbumName.ToLower(), $"% {prefix}%"))  // word after space
            // .OrderByDescending(e => e.PlayCount)
            .Take(500)
            .Include(e => e.Author)
            .ToListAsync();
        
        var ranked = candidates
            .Select(e => new
            {
                Entity = e,
                Distance = StringSimilarity<AlbumEntity>.LevenshteinDistance(e.AlbumName.ToLower(), lowerSearchTerm)
            })
            .OrderBy(x => x.Distance)
            .ToList();

        var similarAlbums = ranked
            .Take(5)
            .Select(x => x.Entity)
            .ToList();

        var best = ranked.FirstOrDefault();
        return (similarAlbums, best?.Entity, best?.Distance ?? int.MaxValue);
    }
}