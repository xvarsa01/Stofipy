using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;

namespace Stofipy.DAL.Repositories;

public class AlbumRepository(StofipyDbContext dbContext) : RepositoryBase<AlbumEntity>(dbContext)
{
    private readonly DbSet<AlbumEntity> _dbSet = dbContext.Set<AlbumEntity>();
}