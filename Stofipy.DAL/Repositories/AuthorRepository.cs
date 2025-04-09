using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;

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

    public async Task<List<AuthorEntity>> SearchInAuthorsAsync(string searchTerm)
    {
        return await _dbSet
            .Where(e => e.AuthorName.ToLower().Contains(searchTerm.ToLower()))
            .ToListAsync();
    }
}