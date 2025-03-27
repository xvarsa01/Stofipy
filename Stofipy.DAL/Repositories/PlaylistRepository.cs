using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;

namespace Stofipy.DAL.Repositories;

public class PlaylistRepository(StofipyDbContext dbContext) : RepositoryBase<PlaylistEntity>(dbContext)
{
    private readonly DbSet<PlaylistEntity> _dbSet = dbContext.Set<PlaylistEntity>();

}