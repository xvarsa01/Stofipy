﻿using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;

namespace Stofipy.DAL.Repositories;

public class AuthorRepository(StofipyDbContext dbContext) : RepositoryBase<AuthorEntity>(dbContext)
{
    private readonly DbSet<AuthorEntity> _dbSet = dbContext.Set<AuthorEntity>();
}