using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;
using Stofipy.DAL.Utilities;

namespace Stofipy.DAL.Repositories;

public class ProfileRepository(StofipyDbContext dbContext) : RepositoryBase<ProfileEntity>(dbContext)
{
    private readonly DbSet<ProfileEntity> _dbSet = dbContext.Set<ProfileEntity>();
    
}