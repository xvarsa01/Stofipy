using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;
using Stofipy.DAL.Utilities;

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
    
    public async Task<List<FileEntity>> GetMostPopularByAuthorAsync(Guid authorId, int pageNumber, int pageSize)
    {
        // TODO currently orderer just by name, as there is no popularity property
        return await _dbSet
            .Where(e => e.AuthorId == authorId)
            .OrderBy(e => e.FileName)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(e => e.Author)
            .ToListAsync();
    }

    public async Task<(List<FileEntity> SimilarFiles, FileEntity? BestFile, int BestDifference)> SearchInFilesAsync(string searchTerm)
    {
        var lowerSearchTerm = searchTerm.ToLower();
        var prefix = searchTerm.Length >= 2 
            ? searchTerm[..2].ToLower() 
            : searchTerm.ToLower();
        
        var candidates = await _dbSet
            .Where(e => EF.Functions.Like(e.FileName.ToLower(), $"{prefix}%")  // word at start
                        || EF.Functions.Like(e.FileName.ToLower(), $"% {prefix}%"))  // word after space
            .OrderByDescending(e => e.PlayCount)
            .Take(500)
            .Include(e => e.Author)
            .ToListAsync();
        
        var ranked = candidates
            .Select(e => new
            {
                Entity = e,
                Distance = StringSimilarity<FileEntity>.LevenshteinDistance(e.FileName.ToLower(), lowerSearchTerm)
            })
            .OrderBy(x => x.Distance)
            .ToList();

        var similarFiles = ranked
            .Take(5)
            .Select(x => x.Entity)
            .ToList();

        var best = ranked.FirstOrDefault();
        return (similarFiles, best?.Entity, best?.Distance ?? int.MaxValue);
    }
}