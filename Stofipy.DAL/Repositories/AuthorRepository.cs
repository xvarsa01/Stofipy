using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;
using Stofipy.DAL.Utilities;

namespace Stofipy.DAL.Repositories;

public class AuthorRepository(StofipyDbContext dbContext) : RepositoryBase<AuthorEntity>(dbContext)
{
    private readonly DbSet<AuthorEntity> _dbSet = dbContext.Set<AuthorEntity>();

    public override async Task<AuthorEntity?> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .Include(e => e.Albums)
            .Include(e => e.Files)
            .SingleOrDefaultAsync(author => author.Id == id)
            ;
    }

    public async Task<(List<AuthorEntity> SimilarAuthors, AuthorEntity? BestAuthor, int BestDifference)> SearchInAuthorsAsync(string searchTerm)
    {
        var lowerSearchTerm = searchTerm.ToLower();
        var prefix = searchTerm.Length >= 2 
            ? searchTerm[..2].ToLower() 
            : searchTerm.ToLower();
        
        var candidates = await _dbSet
            .Where(e => EF.Functions.Like(e.AuthorName.ToLower(), $"{prefix}%")  // word at start
                 || EF.Functions.Like(e.AuthorName.ToLower(), $"% {prefix}%"))  // word after space
            // .OrderByDescending(e => e.PlayCount)
            .Take(500)
            .ToListAsync();
        
        var ranked = candidates
            .Select(e => new
            {
                Entity = e,
                Distance = StringSimilarity<AuthorEntity>.LevenshteinDistance(e.AuthorName.ToLower(), lowerSearchTerm)
            })
            .OrderBy(x => x.Distance)
            .ToList();

        var similarAuthors = ranked
            .Take(5)
            .Select(x => x.Entity)
            .ToList();

        var best = ranked.FirstOrDefault();
        return (similarAuthors, best?.Entity, best?.Distance ?? int.MaxValue);
    }
}