using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;
using Stofipy.DAL.Utilities;

namespace Stofipy.DAL.Repositories;

public class ProfileFollowingAuthorRepository(StofipyDbContext dbContext) : RepositoryBase<ProfileFollowingAuthorEntity>(dbContext)
{
    private readonly DbSet<ProfileFollowingAuthorEntity> _dbSet = dbContext.Set<ProfileFollowingAuthorEntity>();
    
    public async Task<ProfileFollowingAuthorEntity?> GetByIdAsync(Guid followerId, Guid authorId)
    {
        return await _dbSet.SingleOrDefaultAsync(entity => entity.FollowerId == followerId && entity.AuthorId == authorId);
    }
}