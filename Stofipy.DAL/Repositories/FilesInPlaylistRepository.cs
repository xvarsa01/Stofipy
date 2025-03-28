using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;

namespace Stofipy.DAL.Repositories;

public class FilesInPlaylistRepository (StofipyDbContext dbContext): RepositoryBase<FilesInPlaylistEntity>(dbContext)
{
    private readonly DbSet<FilesInPlaylistEntity> _dbSet = dbContext.Set<FilesInPlaylistEntity>();

    public override async Task<List<FilesInPlaylistEntity>> GetAllAsync()
    {
        return await _dbSet
            .Include(e => e.File)
            .ThenInclude(e => e.DefaultAlbum)
            .ToListAsync();
    }
    public async Task<List<FilesInPlaylistEntity>> GetAllAsync(int pageNumber, int pageSize, Guid playlistId)
    {
        return await _dbSet
            .Where(fileInPlaylist => fileInPlaylist.PlaylistId == playlistId )
            .OrderBy(e => e.IndexActual)
            .Include(e => e.File)
            .ThenInclude(e => e.DefaultAlbum)
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();
    }
    
    public async Task<List<FilesInPlaylistEntity>> SearchInPlaylistAsync(Guid playlistId,string searchTerm)
    {
        return await _dbSet
            .Where(fileInPlaylist => fileInPlaylist.PlaylistId == playlistId )
            .Where(fileInPlaylist => fileInPlaylist.File.FileName.ToLower().Contains(searchTerm.ToLower()))
            .Include(e => e.File)
            .ThenInclude(e => e.DefaultAlbum)
            .ToListAsync();
    }
    
    public async Task SortByDurationAsync(Guid playlistId)
    {
        var files = _dbSet
            .Where(fileInPlaylist => fileInPlaylist.PlaylistId == playlistId)
            .Include(e => e.File)
            .OrderBy(e => e.IndexActual)
            .ThenBy(fileInPlaylist => fileInPlaylist.File.Length)
            .ToList();
        
        var index = 1;
        foreach (var file in files)
        {
            file.IndexActual = index;
            await UpdateAsync(file);
            index++;
        }
    }
    
    public async Task SortByTitleAsync(Guid playlistId)
    {
        var files = _dbSet
            .Where(fileInPlaylist => fileInPlaylist.PlaylistId == playlistId)
            .Include(e => e.File)
            .OrderBy(e => e.IndexActual)
            .ThenBy(fileInPlaylist => fileInPlaylist.File.FileName)
            .ToList();
        
        var index = 1;
        foreach (var file in files)
        {
            file.IndexActual = index;
            await UpdateAsync(file);
            index++;
        }
    }
    
    public async Task SortByArtistAsync(Guid playlistId)
    {
        var files = _dbSet
            .Where(fileInPlaylist => fileInPlaylist.PlaylistId == playlistId)
            .Include(e => e.File)
            .ThenInclude(e => e.Author)
            .OrderBy(e => e.IndexActual)
            .ThenBy(fileInPlaylist => fileInPlaylist.File.Author.AuthorName)
            .ToList();
        
        var index = 1;
        foreach (var file in files)
        {
            file.IndexActual = index;
            await UpdateAsync(file);
            index++;
        }
    }
    
    public async Task SortByAlbumAsync(Guid playlistId)
    {
        var files = _dbSet
            .Where(fileInPlaylist => fileInPlaylist.PlaylistId == playlistId)
            .Include(e => e.File)
            .ThenInclude(e => e.DefaultAlbum)
            .OrderBy(fileInPlaylist => fileInPlaylist.File.DefaultAlbum != null ? fileInPlaylist.File.DefaultAlbum.AlbumName : string.Empty)
            .ThenBy(e => e.IndexActual)
            .ToList();

        
        var index = 1;
        foreach (var file in files)
        {
            file.IndexActual = index;
            await UpdateAsync(file);
            index++;
        }
    }
    
    public async Task SortByCustomOrderAsync(Guid playlistId)
    {
        var files = _dbSet
            .Where(fileInPlaylist => fileInPlaylist.PlaylistId == playlistId)
            .Include(e => e.File)
            .OrderBy(fileInPlaylist => fileInPlaylist.IndexCustom)
            .ToList();
        
        var index = 1;
        foreach (var file in files)
        {
            file.IndexActual = index;
            await UpdateAsync(file);
            index++;
        }
    }
}