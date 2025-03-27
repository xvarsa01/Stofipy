using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;

namespace Stofipy.DAL.Repositories;

public class FilesInAlbumRepository(StofipyDbContext dbContext) : RepositoryBase<FilesInAlbumEntity>(dbContext)
{
    private readonly DbSet<FilesInAlbumEntity> _dbSet = dbContext.Set<FilesInAlbumEntity>();
}