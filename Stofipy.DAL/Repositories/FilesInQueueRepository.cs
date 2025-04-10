﻿using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;

namespace Stofipy.DAL.Repositories;

public class FilesInQueueRepository (StofipyDbContext dbContext): RepositoryBase<FilesInQueueEntity>(dbContext)
{
    private readonly DbSet<FilesInQueueEntity> _dbSet = dbContext.Set<FilesInQueueEntity>();

    public override async Task<List<FilesInQueueEntity>> GetAllAsync()
    {
        return await _dbSet
            .Include(e => e.File)
            .ThenInclude(e => e.Author)
            .OrderBy(e => e.Index)
            .ToListAsync();
    }
    public async Task<List<FilesInQueueEntity>> GetAllPriorityAsync()
    {
        return await _dbSet
            .Where(e => e.PriorityQueue == true)
            .Include(e => e.File)
            .ThenInclude(e => e.Author)
            .OrderBy(e => e.Index)
            .ToListAsync();
    }
    
    public async Task<List<FilesInQueueEntity>> GetAllNonPriorityAsync()
    {
        return await _dbSet
            .Where(e => e.PriorityQueue == false)
            .Include(e => e.File)
            .ThenInclude(e => e.Author)
            .OrderBy(e => e.Index)
            .ToListAsync();
    }

    public async Task<FilesInQueueEntity?> GetByIndexAsync(int index, bool priority)
    {
        if (priority)
        {
            return await _dbSet
                .Where(e => e.PriorityQueue == true)
                .SingleAsync(e => e.Index == index);
        }
        else
        {
            return await _dbSet
                .Where(e => e.PriorityQueue == false)
                .SingleAsync(e => e.Index == index);
        }
    }

    public int GetMaxPriorityIndex()
    {
        return _dbSet
            .Where(e => e.PriorityQueue == true)
            .Select(x => (int?)x.Index)
            .Max() ?? 0;
    }
    public int GetMaxNonPriorityIndex()
    {
        return _dbSet
            .Where(e => e.PriorityQueue == false)
            .Select(x => (int?)x.Index)
            .Max() ?? 0;
    }
}