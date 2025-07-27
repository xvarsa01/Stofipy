using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;
using Stofipy.DAL.Utilities;

namespace Stofipy.DAL.Repositories;

public class ProfileFollowingProfileRepository(StofipyDbContext dbContext) : RepositoryBase<ProfileFollowingProfileEntity>(dbContext)
{
    private readonly DbSet<ProfileFollowingProfileEntity> _dbSet = dbContext.Set<ProfileFollowingProfileEntity>();

    public async Task<ProfileFollowingProfileEntity?> GetByIdAsync(Guid followerId, Guid followedId)
    {
        return await _dbSet.SingleOrDefaultAsync(entity => entity.FollowerId == followerId && entity.FollowedId == followedId);
    }
}