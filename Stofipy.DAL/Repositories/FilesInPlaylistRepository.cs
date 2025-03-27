using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;

namespace Stofipy.DAL.Repositories;

public class FilesInPlaylistRepository (StofipyDbContext dbContext): RepositoryBase<FilesInPlaylistEntity>(dbContext)
{
    private readonly DbSet<FilesInPlaylistEntity> _dbSet = dbContext.Set<FilesInPlaylistEntity>();

    public async Task<List<FilesInPlaylistEntity>> SearchInPlaylistAsync(Guid playlistId,string searchTerm)
    {
        return await _dbSet
            .Where(fileInPlaylist => fileInPlaylist.PlaylistId == playlistId )
            .Where(fileInPlaylist => fileInPlaylist.File.FileName.ToLower().Contains(searchTerm.ToLower()))
            .ToListAsync();
    }
    
    public async Task SortByDurationAsync(Guid playlistId)
    {
        var files = _dbSet
            .Where(fileInPlaylist => fileInPlaylist.PlaylistId == playlistId)
            .OrderBy(fileInPlaylist => fileInPlaylist.File.Length);
        
        var index = 1;
        foreach (var file in files)
        {
            file.IndexActual = index;
            await UpdateAsync(file);
            index++;
        }
    }
}